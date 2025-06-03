using AutoMapper;
using SocialNetwork.Contracts.Requests;
using SocialNetwork.Contracts.Responses;
using SocialNetwork.Core.Repositories;
using SocialNetwork.Core.Services;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Exceptions;
using SocialNetwork.Infrastructure.Entities;

namespace SocialNetwork.Api.Services;

/// <summary>
/// Implementation of the <see cref="IUserService"/> interface.
/// Provides business logic for user-related operations such as creation, retrieval, and listing.
/// </summary>
public class UserService : IUserService
{
	private readonly IUserRepository _userRepository;
	private readonly IMapper _mapper;

	/// <summary>
	/// Initializes a new instance of the <see cref="UserService"/> class.
	/// </summary>
	/// <param name="userRepository">The repository used to perform data operations on users.</param>
	/// <param name="mapper">The mapper used to convert between domain models and DTOs.</param>
	public UserService(IUserRepository userRepository, IMapper mapper)
	{
		_userRepository = userRepository;
		_mapper = mapper;
	}

	/// <summary>
	/// Creates a new user based on the provided request data.
	/// Maps the request to a domain model, saves it using the repository, and returns the response.
	/// </summary>
	/// <param name="request">The request containing the user's name.</param>
	/// <returns>
	/// A task that represents the asynchronous operation. 
	/// The task result contains a <see cref="UserResponse"/> object representing the created user.
	/// </returns>
	/// <exception cref="ValidationException">Thrown when validation fails (e.g., empty name).</exception>
	public async Task<UserResponse> CreateUserAsync(CreateUserRequest request)
	{
		var user = new User { Name = request.Name };
		var userDb = _mapper.Map<UserDb>(user); // Converts domain model to persistence model

		await _userRepository.AddAsync(userDb);
		return _mapper.Map<UserResponse>(userDb);
	}

	/// <summary>
	/// Retrieves all registered users asynchronously and maps them to response DTOs.
	/// </summary>
	/// <returns>
	/// A task that represents the asynchronous operation. 
	/// The task result contains a collection of <see cref="UserResponse"/> objects representing all users.
	/// </returns>
	public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
	{
		var users = await _userRepository.GetAllAsync(); // Returns IEnumerable<UserDb>

		return users.Select(u => new UserResponse
		{
			Id = u.Id,
			Name = u.Name
		});
	}

	/// <summary>
	/// Retrieves a user by their unique identifier and maps it to a response DTO.
	/// Throws an exception if the user is not found.
	/// </summary>
	/// <param name="id">The unique identifier of the user to retrieve.</param>
	/// <returns>
	/// A task that represents the asynchronous operation. 
	/// The task result contains a <see cref="UserResponse"/> object representing the user.
	/// </returns>
	/// <exception cref="UserNotFoundException">Thrown if the user with the specified ID does not exist.</exception>
	public async Task<UserResponse> GetUserByIdAsync(Guid id)
	{
		var userDb = await _userRepository.GetByIdAsync(id);
		if (userDb == null)
			throw new UserNotFoundException(id);

		return _mapper.Map<UserResponse>(userDb);
	}
}