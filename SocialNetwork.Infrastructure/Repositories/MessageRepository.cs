using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Repositories;
using SocialNetwork.Infrastructure.Data;
using SocialNetwork.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Repositories
{
	/// <summary>
	/// Implementation of the <see cref="IMessageRepository"/> interface.
	/// Provides data access logic for message-related operations in the database.
	/// </summary>
	public class MessageRepository : IMessageRepository
	{
		private readonly AppDbContext _context;

		/// <summary>
		/// Initializes a new instance of the <see cref="MessageRepository"/> class.
		/// </summary>
		/// <param name="context">The database context to use for data operations.</param>
		public MessageRepository(AppDbContext context) => _context = context;

		/// <summary>
		/// Retrieves a message by its unique identifier asynchronously.
		/// </summary>
		/// <param name="id">The unique identifier of the message to retrieve.</param>
		/// <returns>
		/// A task that represents the asynchronous operation. 
		/// The task result contains the message entity if found, or null if not found.
		/// </returns>
		public async Task<MessageDb?> GetByIdAsync(Guid id) =>
			await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);

		/// <summary>
		/// Retrieves all messages from the database asynchronously.
		/// </summary>
		/// <returns>
		/// A task that represents the asynchronous operation. 
		/// The task result contains a collection of all messages in the system.
		/// </returns>
		public async Task<IEnumerable<MessageDb>> GetAllAsync() =>
			await _context.Messages.ToListAsync();

		/// <summary>
		/// Adds a new message to the database asynchronously.
		/// </summary>
		/// <param name="message">The message entity to add.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		public async Task AddAsync(MessageDb message)
		{
			await _context.Messages.AddAsync(message);
		}

		/// <summary>
		/// Saves all changes made in this repository to the database asynchronously.
		/// </summary>
		/// <returns>A task that represents the asynchronous save operation.</returns>
		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}