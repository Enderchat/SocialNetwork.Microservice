using Microsoft.EntityFrameworkCore;
using SocialNetwork.Contracts.Requests;
using SocialNetwork.Contracts.Responses;
using SocialNetwork.Core.Repositories;
using SocialNetwork.Core.Services;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Exceptions;
using SocialNetwork.Infrastructure.Data;
using SocialNetwork.Infrastructure.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Services
{
	/// <summary>
	/// Implementation of the <see cref="IMessageService"/> interface.
	/// Provides business logic for sending messages and retrieving message history between users.
	/// </summary>
	public class MessageService : IMessageService
	{
		private readonly AppDbContext _context;
		private readonly IUserRepository _userRepository;

		/// <summary>
		/// Initializes a new instance of the <see cref="MessageService"/> class.
		/// </summary>
		/// <param name="context">The database context used for data operations.</param>
		/// <param name="userRepository">The repository used to access user data.</param>
		public MessageService(AppDbContext context, IUserRepository userRepository)
		{
			_context = context;
			_userRepository = userRepository;
		}

		/// <summary>
		/// Sends a message from one user to another asynchronously.
		/// Validates that both users exist and updates their message lists accordingly.
		/// </summary>
		/// <param name="senderId">The ID of the user sending the message.</param>
		/// <param name="receiverId">The ID of the user receiving the message.</param>
		/// <param name="content">The content of the message being sent.</param>
		/// <returns>
		/// A task that represents the asynchronous operation. 
		/// The task result contains a <see cref="MessageResponse"/> object representing the sent message.
		/// </returns>
		/// <exception cref="UserNotFoundException">Thrown if either sender or receiver does not exist.</exception>
		public async Task<MessageResponse> SendMessageAsync(Guid senderId, Guid receiverId, string content)
		{
			var sender = await _userRepository.GetByIdAsync(senderId)
						   ?? throw new UserNotFoundException(senderId);

			var receiver = await _userRepository.GetByIdAsync(receiverId)
						   ?? throw new UserNotFoundException(receiverId);

			var messageDb = new MessageDb
			{
				Id = Guid.NewGuid(),
				SenderId = senderId,
				ReceiverId = receiverId,
				Content = content,
				SentAt = DateTime.UtcNow
			};

			// Load users with related messages to update collections
			var dbSender = await _context.Users
				.Include(u => u.SentMessages)
				.FirstOrDefaultAsync(u => u.Id == senderId);

			var dbReceiver = await _context.Users
				.Include(u => u.ReceivedMessages)
				.FirstOrDefaultAsync(u => u.Id == receiverId);

			if (dbSender == null || dbReceiver == null)
				throw new UserNotFoundException(dbSender == null ? senderId : receiverId);

			dbSender.SentMessages.Add(messageDb);
			dbReceiver.ReceivedMessages.Add(messageDb);

			_context.Users.Update(dbSender);
			_context.Users.Update(dbReceiver);
			await _context.SaveChangesAsync();

			return new MessageResponse
			{
				Id = messageDb.Id,
				SenderId = messageDb.SenderId,
				ReceiverId = messageDb.ReceiverId,
				Content = messageDb.Content,
				SentAt = messageDb.SentAt
			};
		}

		/// <summary>
		/// Retrieves all messages exchanged between two users asynchronously.
		/// Converts the retrieved message entities into response DTOs for client consumption.
		/// </summary>
		/// <param name="id1">The ID of the first user.</param>
		/// <param name="id2">The ID of the second user.</param>
		/// <returns>
		/// A task that represents the asynchronous operation. 
		/// The task result contains a collection of <see cref="MessageResponse"/> objects.
		/// </returns>
		public async Task<IEnumerable<MessageResponse>> GetMessagesBetweenAsync(Guid id1, Guid id2)
		{
			var messages = await _userRepository.GetMessagesAsync(id1, id2);
			return messages.Select(m => new MessageResponse
			{
				Id = m.Id,
				SenderId = m.SenderId,
				ReceiverId = m.ReceiverId,
				Content = m.Content,
				SentAt = m.SentAt
			});
		}
	}
}