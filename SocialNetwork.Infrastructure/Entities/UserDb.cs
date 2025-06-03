namespace SocialNetwork.Infrastructure.Entities;

/// <summary>
/// Represents a user in the database.
/// Contains basic user information and relationships such as friendships and messages.
/// </summary>
public class UserDb
{
	/// <summary>
	/// Gets or sets the unique identifier of the user.
	/// Defaults to a new GUID if not explicitly set.
	/// </summary>
	public Guid Id { get; set; } = Guid.NewGuid();

	/// <summary>
	/// Gets or sets the name of the user.
	/// Defaults to an empty string if not explicitly set.
	/// </summary>
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the collection of friendships associated with this user.
	/// Each friendship represents a relationship with another user.
	/// </summary>
	public ICollection<FriendshipDb> Friendships { get; set; } = new List<FriendshipDb>();

	/// <summary>
	/// Gets or sets the collection of messages sent by this user.
	/// </summary>
	public ICollection<MessageDb> SentMessages { get; set; } = new List<MessageDb>();

	/// <summary>
	/// Gets or sets the collection of messages received by this user.
	/// </summary>
	public ICollection<MessageDb> ReceivedMessages { get; set; } = new List<MessageDb>();

	/// <summary>
	/// Checks whether the current user is friends with the user having the specified ID.
	/// </summary>
	/// <param name="friendId">The ID of the user to check friendship status with.</param>
	/// <returns>True if the users are friends; otherwise, false.</returns>
	public bool IsFriend(Guid friendId) => Friendships.Any(f => f.FriendId == friendId);

	/// <summary>
	/// Adds a new friendship relationship between this user and another user.
	/// If the friendship already exists, it will not be added again.
	/// </summary>
	/// <param name="friend">The user to add as a friend.</param>
	public void AddFriend(UserDb friend)
	{
		if (!IsFriend(friend.Id))
			Friendships.Add(new FriendshipDb { UserId = Id, FriendId = friend.Id });
	}
}