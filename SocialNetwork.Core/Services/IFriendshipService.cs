using SocialNetwork.Contracts.Responses;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Services
{
	/// <summary>
	/// Defines the business logic for managing friendships between users.
	/// </summary>
	public interface IFriendshipService
	{
		/// <summary>
		/// Adds a friendship relationship between two users asynchronously.
		/// </summary>
		/// <param name="userId">The ID of the user initiating the friend request.</param>
		/// <param name="friendId">The ID of the user being added as a friend.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		Task AddFriendAsync(Guid userId, Guid friendId);

		/// <summary>
		/// Removes a friendship relationship between two users asynchronously.
		/// </summary>
		/// <param name="userId">The ID of the user whose friendship is being removed.</param>
		/// <param name="friendId">The ID of the friend to remove.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		Task RemoveFriendAsync(Guid userId, Guid friendId);

		/// <summary>
		/// Checks whether two users are friends asynchronously.
		/// </summary>
		/// <param name="userId">The ID of the first user.</param>
		/// <param name="friendId">The ID of the second user to check against.</param>
		/// <returns>
		/// A task that represents the asynchronous operation. 
		/// The task result contains a boolean indicating if the users are friends.
		/// </returns>
		Task<bool> AreFriendsAsync(Guid userId, Guid friendId);

		/// <summary>
		/// Retrieves a list of all friends for a given user asynchronously.
		/// </summary>
		/// <param name="userId">The ID of the user whose friends are being retrieved.</param>
		/// <returns>
		/// A task that represents the asynchronous operation. 
		/// The task result contains a list of <see cref="UserResponse"/> objects representing the user's friends.
		/// </returns>
		Task<List<UserResponse>> GetFriendsAsync(Guid userId);
	}
}