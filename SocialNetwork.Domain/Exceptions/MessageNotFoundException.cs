using SocialNetwork.Domain.Exceptions;

namespace SocialNetwork.Domain.Exceptions
{
	/// <summary>
	/// Represents an exception that is thrown when a specified user is not found in another user's friend list.
	/// </summary>
	public class FriendNotFoundException : BaseException
	{
		/// <summary>
		/// Gets the ID of the user whose friend list is being checked.
		/// </summary>
		public Guid UserId { get; }

		/// <summary>
		/// Gets the ID of the user that was expected to be a friend but was not found.
		/// </summary>
		public Guid FriendId { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="FriendNotFoundException"/> class 
		/// with a message indicating that the specified user is not a friend.
		/// </summary>
		/// <param name="userId">The ID of the user whose friend list is being checked.</param>
		/// <param name="friendId">The ID of the user who was expected to be a friend.</param>
		public FriendNotFoundException(Guid userId, Guid friendId)
			: base($"User with ID {userId} is not friends with user {friendId}.")
		{
			UserId = userId;
			FriendId = friendId;
		}
	}
}