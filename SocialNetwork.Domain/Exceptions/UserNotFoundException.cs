using SocialNetwork.Domain.Exceptions;

namespace SocialNetwork.Domain.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a requested user is not found in the system.
/// This exception includes the ID of the missing user for diagnostic purposes.
/// </summary>
public class UserNotFoundException : BaseException
{
	/// <summary>
	/// Gets the unique identifier of the user that was not found.
	/// </summary>
	public Guid UserId { get; }

	/// <summary>
	/// Initializes a new instance of the <see cref="UserNotFoundException"/> class 
	/// with a message indicating that the user with the specified ID was not found.
	/// </summary>
	/// <param name="userId">The ID of the user that was not found.</param>
	public UserNotFoundException(Guid userId) : base($"User with ID {userId} not found.")
	{
		UserId = userId;
	}
}