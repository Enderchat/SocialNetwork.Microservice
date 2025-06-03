namespace SocialNetwork.Infrastructure.Entities;

/// <summary>
/// Represents a friendship relationship between two users in the database.
/// This class is used by Entity Framework Core for mapping to the corresponding database table.
/// </summary>
public class FriendshipDb
{
	/// <summary>
	/// Gets or sets the unique identifier of the user who initiated the friendship.
	/// This property represents the primary key of the user in the relationship.
	/// </summary>
	public Guid UserId { get; set; }

	/// <summary>
	/// Gets or sets the unique identifier of the user who is the friend.
	/// This property represents the foreign key to the user being added as a friend.
	/// </summary>
	public Guid FriendId { get; set; }

	/// <summary>
	/// Gets or sets the navigation property for the user who initiated the friendship.
	/// This allows Entity Framework Core to navigate from a friendship to the associated user.
	/// </summary>
	public UserDb? User { get; set; }

	/// <summary>
	/// Gets or sets the navigation property for the friend in this friendship relationship.
	/// This allows Entity Framework Core to navigate from a friendship to the associated friend.
	/// </summary>
	public UserDb? Friend { get; set; }
}