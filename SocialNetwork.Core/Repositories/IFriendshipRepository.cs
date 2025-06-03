using SocialNetwork.Infrastructure.Entities;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Repositories
{
	/// <summary>
	/// Defines the contract for managing friendship data operations in the social network.
	/// This interface provides methods to add, remove, retrieve, and check friendships between users.
	/// </summary>
	public interface IFriendshipRepository
	{
		/// <summary>
		/// Adds a new friendship relationship between two users.
		/// </summary>
		/// <param name="friendship">The friendship entity containing user and friend IDs.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		Task AddAsync(FriendshipDb friendship);

		/// <summary>
		/// Removes a friendship relationship between two users.
		/// </summary>
		/// <param name="userId">The ID of the user whose friendship is being removed.</param>
		/// <param name="friendId">The ID of the friend to remove.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		Task RemoveAsync(Guid userId, Guid friendId);

		/// <summary>
		/// Checks whether a friendship exists between two users.
		/// </summary>
		/// <param name="userId">The ID of the first user.</param>
		/// <param name="friendId">The ID of the second user to check against.</param>
		/// <returns>
		/// A task that represents the asynchronous operation. 
		/// The task result contains a boolean indicating whether the users are friends.
		/// </returns>
		Task<bool> AreFriendsAsync(Guid userId, Guid friendId);

		/// <summary>
		/// Retrieves all friendship relationships for a given user.
		/// </summary>
		/// <param name="userId">The ID of the user whose friends are being retrieved.</param>
		/// <returns>
		/// A task that represents the asynchronous operation. 
		/// The task result contains a collection of friendship entities.
		/// </returns>
		Task<IEnumerable<FriendshipDb>> GetFriendsAsync(Guid userId);

		/// <summary>
		/// Saves changes made to the repository (e.g., database).
		/// </summary>
		/// <returns>A task that represents the asynchronous save operation.</returns>
		Task SaveChangesAsync();
	}
}