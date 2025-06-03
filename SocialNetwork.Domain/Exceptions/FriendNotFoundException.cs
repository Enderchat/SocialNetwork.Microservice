using SocialNetwork.Domain.Exceptions;

namespace SocialNetwork.Domain.Exceptions
{
	/// <summary>
	/// Represents an exception that is thrown when a friendship relationship between two users does not exist.
	/// </summary>
	public class FriendshipNotFoundException : BaseException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FriendshipNotFoundException"/> class 
		/// with a message indicating that no friendship was found between the specified users.
		/// </summary>
		/// <param name="userId">The ID of the user whose friendship is being checked.</param>
		/// <param name="friendId">The ID of the friend to check against.</param>
		public FriendshipNotFoundException(Guid userId, Guid friendId)
			: base($"Friendship between user {userId} and user {friendId} not found.")
		{
			// No additional implementation required — message is provided via base constructor
		}
	}
}