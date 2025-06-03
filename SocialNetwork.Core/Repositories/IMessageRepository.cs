using SocialNetwork.Infrastructure.Entities;

namespace SocialNetwork.Core.Repositories;

/// <summary>
/// Defines the contract for message data access operations in the social network application.
/// This interface provides methods to retrieve, add, and persist messages in the data store.
/// </summary>
public interface IMessageRepository
{
	/// <summary>
	/// Retrieves a message by its unique identifier.
	/// </summary>
	/// <param name="id">The unique identifier of the message to retrieve.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the message entity if found.</returns>
	Task<MessageDb> GetByIdAsync(Guid id);

	/// <summary>
	/// Retrieves all messages from the data store.
	/// </summary>
	/// <returns>A task that represents the asynchronous operation. The task result contains a collection of all messages.</returns>

	Task<IEnumerable<MessageDb>> GetAllAsync();

	/// <summary>
	/// Adds a new message to the data store.
	/// </summary>
	/// <param name="message">The message entity to add.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	Task AddAsync(MessageDb message);

	/// <summary>
	/// Saves changes made to the message repository (e.g., database).
	/// </summary>
	/// <returns>A task that represents the asynchronous save operation.</returns>
	Task SaveChangesAsync();
}