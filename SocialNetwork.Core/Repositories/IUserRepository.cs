using SocialNetwork.Contracts.Responses;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Infrastructure.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Repositories
{
	/// <summary>
	/// Defines the contract for user data access operations.
	/// This interface provides methods to retrieve, add, update, delete users, and manage their messages.
	/// </summary>
	public interface IUserRepository
	{
		/// <summary>
		/// Retrieves a user by their unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the user.</param>
		/// <returns>A task that represents the asynchronous operation. The task result contains the user entity if found.</returns>
		Task<UserDb?> GetByIdAsync(Guid id);

		/// <summary>
		/// Retrieves all users from the data store.
		/// </summary>
		/// <returns>A task that represents the asynchronous operation. The task result contains a collection of all user entities.</returns>
		Task<IEnumerable<UserDb>> GetAllAsync();

		/// <summary>
		/// Adds a new user to the data store.
		/// </summary>
		/// <param name="user">The user entity to add.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		Task AddAsync(UserDb user);

		/// <summary>
		/// Updates an existing user in the data store.
		/// </summary>
		/// <param name="user">The updated user entity.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		Task UpdateAsync(UserDb user);

		/// <summary>
		/// Deletes a user from the data store by their ID.
		/// </summary>
		/// <param name="id">The unique identifier of the user to delete.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		Task DeleteAsync(Guid id);

		/// <summary>
		/// Saves changes made to the repository (e.g., database).
		/// </summary>
		/// <returns>A task that represents the asynchronous save operation.</returns>
		Task SaveChangesAsync();

		/// <summary>
		/// Retrieves all messages exchanged between two users.
		/// </summary>
		/// <param name="id1">The first user's ID.</param>
		/// <param name="id2">The second user's ID.</param>
		/// <returns>
		/// A task that represents the asynchronous operation. 
		/// The task result contains a collection of message entities between the two users.
		/// </returns>
		Task<IEnumerable<MessageDb>> GetMessagesAsync(Guid id1, Guid id2);
	}
}