using SocialNetwork.Contracts.Responses;
using SocialNetwork.Core.Repositories;
using SocialNetwork.Core.Services;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Exceptions;
using SocialNetwork.Infrastructure.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Services
{
	/// <summary>
	/// Implementation of the <see cref="IFriendshipService"/> interface.
	/// Provides business logic for managing user friendships in the social network.
	/// </summary>
	public class FriendshipService : IFriendshipService
	{
		private readonly IUserRepository _userRepository;
		private readonly IFriendshipRepository _friendshipRepository;

		/// <summary>
		/// Initializes a new instance of the <see cref="FriendshipService"/> class.
		/// </summary>
		/// <param name="userRepository">The repository used to access user data.</param>
		/// <param name="friendshipRepository">The repository used to manage friendship relationships.</param>
		public FriendshipService(
			IUserRepository userRepository,
			IFriendshipRepository friendshipRepository)
		{
			_userRepository = userRepository;
			_friendshipRepository = friendshipRepository;
		}

		/// <summary>
		/// Adds a bidirectional friendship between two users asynchronously.
		/// Validates that both users exist and are not already friends before adding the relationship.
		/// </summary>
		/// <param name="userId">The ID of the user initiating the friendship request.</param>
		/// <param name="friendId">The ID of the user being added as a friend.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		/// <exception cref="UserNotFoundException">Thrown if either user does not exist.</exception>
		/// <exception cref="FriendNotFoundException">Thrown if the friend is not found in the user's list.</exception>
		/// <exception cref="FriendshipExistsException">Thrown if the friendship already exists.</exception>
		public async Task AddFriendAsync(Guid userId, Guid friendId)
		{
			var user = await _userRepository.GetByIdAsync(userId)
					   ?? throw new UserNotFoundException(userId);

			var friend = await _userRepository.GetByIdAsync(friendId)
						 ?? throw new FriendNotFoundException(userId, friendId);

			if (await _friendshipRepository.AreFriendsAsync(userId, friendId))
				throw new FriendshipExistsException(userId, friendId);

			await _friendshipRepository.AddAsync(new FriendshipDb
			{
				UserId = userId,
				FriendId = friendId
			});

			await _friendshipRepository.AddAsync(new FriendshipDb
			{
				UserId = friendId,
				FriendId = userId
			});

			await _userRepository.SaveChangesAsync();
		}

		/// <summary>
		/// Removes a bidirectional friendship relationship between two users asynchronously.
		/// Removes both directions of the friendship if they exist.
		/// </summary>
		/// <param name="userId">The ID of the user whose friendship is being removed.</param>
		/// <param name="friendId">The ID of the friend to remove.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		public async Task RemoveFriendAsync(Guid userId, Guid friendId)
		{
			var user = await _userRepository.GetByIdAsync(userId)
					   ?? throw new UserNotFoundException(userId);

			var friendship = user.Friendships.FirstOrDefault(f => f.FriendId == friendId);
			if (friendship != null)
			{
				user.Friendships.Remove(friendship);
				await _userRepository.UpdateAsync(user);
			}

			var friend = await _userRepository.GetByIdAsync(friendId);
			if (friend != null && friend.IsFriend(userId))
			{
				var reverseFriendship = friend.Friendships.FirstOrDefault(f => f.FriendId == userId);
				if (reverseFriendship != null)
				{
					friend.Friendships.Remove(reverseFriendship);
					await _userRepository.UpdateAsync(friend);
				}
			}

			await _userRepository.SaveChangesAsync();
		}

		/// <summary>
		/// Checks whether a bidirectional friendship exists between two users asynchronously.
		/// </summary>
		/// <param name="userId">The ID of the first user.</param>
		/// <param name="friendId">The ID of the second user.</param>
		/// <returns>
		/// A task that represents the asynchronous operation. 
		/// The task result contains a boolean indicating whether the users are friends.
		/// </returns>
		public async Task<bool> AreFriendsAsync(Guid userId, Guid friendId)
		{
			return await _friendshipRepository.AreFriendsAsync(userId, friendId);
		}

		/// <summary>
		/// Retrieves a list of all friends for a given user asynchronously.
		/// Returns a list of <see cref="UserResponse"/> objects representing each friend.
		/// </summary>
		/// <param name="userId">The ID of the user whose friends are being retrieved.</param>
		/// <returns>
		/// A task that represents the asynchronous operation. 
		/// The task result contains a list of <see cref="UserResponse"/> objects representing the user's friends.
		/// </returns>
		public async Task<List<UserResponse>> GetFriendsAsync(Guid userId)
		{
			var friends = await _friendshipRepository.GetFriendsAsync(userId);
			return friends.Select(f => new UserResponse
			{
				Id = f.Friend!.Id,
				Name = f.Friend.Name
			}).ToList();
		}
	}
}