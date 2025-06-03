namespace SocialNetwork.Domain.Exceptions;

/// <summary>
/// Represents an exception that is thrown when attempting to add a friendship 
/// between two users who are already friends.
/// </summary>
public class FriendshipExistsException : BaseException
{
	/// <summary>
	/// Gets the ID of the user who initiated the friendship request.
	/// </summary>
	public Guid UserId { get; }

	/// <summary>
	/// Gets the ID of the user who is already a friend.
	/// </summary>
	public Guid FriendId { get; }

	/// <summary>
	/// Initializes a new instance of the <see cref="FriendshipExistsException"/> class 
	/// with a message indicating that the friendship already exists.
	/// </summary>
	/// <param name="userId">The ID of the user initiating the friendship.</param>
	/// <param name="friendId">The ID of the user who is already a friend.</param>
	public FriendshipExistsException(Guid userId, Guid friendId)
		: base($"ѕользователь с ID {userId} уже €вл€етс€ другом пользовател€ {friendId}")
	{
		UserId = userId;
		FriendId = friendId;
	}
}