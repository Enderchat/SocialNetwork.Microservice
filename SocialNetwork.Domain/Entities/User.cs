using System.Collections.Generic;

namespace SocialNetwork.Domain.Entities;

/// <summary>
/// Represents a user in the social network.
/// Contains core user properties and business logic for managing friendships.
/// </summary>
public class User
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
	/// Checks whether the current user is friends with the user having the specified ID.
	/// </summary>
	/// <param name="friendId">The ID of the user to check friendship status with.</param>
	/// <returns>True if the users are friends; otherwise, false.</returns>
	public bool IsFriend(Guid friendId)
	{
		return Friendships.Any(f => f.FriendId == friendId);
	}

	/// <summary>
	/// Gets or sets the collection of friendships associated with this user.
	/// Each friendship represents a relationship with another user.
	/// </summary>
	public ICollection<Friendship> Friendships { get; set; } = new List<Friendship>();
}