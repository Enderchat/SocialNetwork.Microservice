using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Repositories;
using SocialNetwork.Domain.Exceptions;
using SocialNetwork.Infrastructure.Data;
using SocialNetwork.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Repositories
{
	/// <summary>
	/// Implementation of the <see cref="IUserRepository"/> interface.
	/// Provides data access logic for user-related operations in the database.
	/// </summary>
	public class UserRepository : IUserRepository
	{
		private readonly AppDbContext _context;

		/// <summary>
		/// Initializes a new instance of the <see cref="UserRepository"/> class.
		/// </summary>
		/// <param name="context">The database context to use for data operations.</param>
		public UserRepository(AppDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Retrieves a user by their unique identifier, including related messages.
		/// </summary>
		/// <param name="id">The unique identifier of the user.</param>
		/// <returns>
		/// A task that represents the asynchronous operation. 
		/// The task result contains the user entity if found, or null if not found.
		/// </returns>
		public async Task<UserDb?> GetByIdAsync(Guid id)
		{
			return await _context.Users
				.Include(u => u.SentMessages)
				.Include(u => u.ReceivedMessages)
				.FirstOrDefaultAsync(u => u.Id == id);
		}

		/// <summary>
		/// Retrieves all users from the database asynchronously.
		/// </summary>
		/// <returns>
		/// A task that represents the asynchronous operation. 
		/// The task result contains a collection of all user entities.
		/// </returns>
		public async Task<IEnumerable<UserDb>> GetAllAsync()
		{
			return await _context.Users.ToListAsync();
		}

		/// <summary>
		/// Adds a new user to the database and saves changes immediately.
		/// </summary>
		/// <param name="user">The user entity to add.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		public async Task AddAsync(UserDb user)
		{
			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();
		}

		/// <summary>
		/// Updates an existing user in the database, including message relationships.
		/// If the user is not found, throws a <see cref="UserNotFoundException"/>.
		/// </summary>
		/// <param name="user">The updated user entity.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		public async Task UpdateAsync(UserDb user)
		{
			var existingUser = await _context.Users
				.Include(u => u.SentMessages)
				.Include(u => u.ReceivedMessages)
				.FirstOrDefaultAsync(u => u.Id == user.Id);

			if (existingUser == null)
				throw new UserNotFoundException(user.Id);

			existingUser.Name = user.Name;

			foreach (var message in user.SentMessages)
			{
				if (!existingUser.SentMessages.Any(m => m.Id == message.Id))
				{
					existingUser.SentMessages.Add(message);
				}
			}

			foreach (var message in user.ReceivedMessages)
			{
				if (!existingUser.ReceivedMessages.Any(m => m.Id == message.Id))
				{
					existingUser.ReceivedMessages.Add(message);
				}
			}

			_context.Users.Update(existingUser);
			await _context.SaveChangesAsync();
		}

		/// <summary>
		/// Deletes a user by their ID asynchronously.
		/// </summary>
		/// <param name="id">The unique identifier of the user to delete.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		public async Task DeleteAsync(Guid id)
		{
			var user = await _context.Users.FindAsync(id);
			if (user != null)
			{
				_context.Users.Remove(user);
				await _context.SaveChangesAsync();
			}
		}

		/// <summary>
		/// Saves all changes made in this repository to the database asynchronously.
		/// </summary>
		/// <returns>A task that represents the asynchronous save operation.</returns>
		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}

		/// <summary>
		/// Retrieves all messages exchanged between two users asynchronously.
		/// Messages are returned in descending order by timestamp.
		/// </summary>
		/// <param name="id1">The ID of the first user.</param>
		/// <param name="id2">The ID of the second user.</param>
		/// <returns>
		/// A task that represents the asynchronous operation. 
		/// The task result contains a collection of message entities exchanged between the users.
		/// </returns>
		public async Task<IEnumerable<MessageDb>> GetMessagesAsync(Guid id1, Guid id2)
		{
			return await _context.Messages
				.Where(m => (m.SenderId == id1 && m.ReceiverId == id2) ||
							(m.SenderId == id2 && m.ReceiverId == id1))
				.OrderByDescending(m => m.SentAt)
				.ToListAsync();
		}
	}
}