namespace SocialNetwork.Domain.Entities;

/// <summary>
/// Represents a friendship relationship between two users in the social network.
/// </summary>
public class Friendship
{
	/// <summary>
	/// Gets or sets the unique identifier of the user who initiated the friendship.
	/// </summary>
	public Guid UserId { get; set; }

	/// <summary>
	/// Gets or sets the unique identifier of the friend in this friendship relationship.
	/// </summary>
	public Guid FriendId { get; set; }

	/// <summary>
	/// Gets or sets the user entity associated with this friendship.
	/// This property is used for navigation and may be null if not loaded.
	/// </summary>
	public User? User { get; set; }

	/// <summary>
	/// Gets or sets the friend entity associated with this friendship.
	/// This property is used for navigation and may be null if not loaded.
	/// </summary>
	public User? Friend { get; set; }
}