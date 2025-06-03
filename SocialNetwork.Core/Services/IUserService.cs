using SocialNetwork.Contracts.Requests;
using SocialNetwork.Contracts.Responses;

namespace SocialNetwork.Core.Services;

/// <summary>
/// Defines the business logic for managing user-related operations.
/// </summary>
public interface IUserService
{
	/// <summary>
	/// Creates a new user asynchronously based on the provided request data.
	/// </summary>
	/// <param name="request">The request containing user creation details.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the created user response.</returns>
	Task<UserResponse> CreateUserAsync(CreateUserRequest request);

	/// <summary>
	/// Retrieves a list of all registered users asynchronously.
	/// </summary>
	/// <returns>A task that represents the asynchronous operation. The task result contains a collection of user responses.</returns>
	Task<IEnumerable<UserResponse>> GetAllUsersAsync();

	/// <summary>
	/// Retrieves a user by their unique identifier asynchronously.
	/// </summary>
	/// <param name="id">The unique identifier of the user to retrieve.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the user response if found.</returns>
	Task<UserResponse> GetUserByIdAsync(Guid id);
}