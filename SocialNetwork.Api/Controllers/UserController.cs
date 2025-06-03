using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Contracts.Requests;
using SocialNetwork.Contracts.Responses;
using SocialNetwork.Core.Services;

namespace SocialNetwork.Api.Controllers;

/// <summary>
/// Controller responsible for handling user-related operations in the social network API.
/// Provides endpoints for creating, retrieving, and listing users.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
	private readonly IUserService _userService;

	/// <summary>
	/// Initializes a new instance of the <see cref="UserController"/> class.
	/// </summary>
	/// <param name="userService">The service used to perform user operations.</param>
	public UserController(IUserService userService) => _userService = userService;

	/// <summary>
	/// Creates a new user based on the provided request data.
	/// </summary>
	/// <param name="request">The request containing the user's name and other required details.</param>
	/// <returns>An <see cref="IActionResult"/> containing the created user's details.</returns>
	[HttpPost]
	public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
	{
		var result = await _userService.CreateUserAsync(request);
		return Ok(result);
	}

	/// <summary>
	/// Retrieves a user by their unique identifier.
	/// </summary>
	/// <param name="id">The ID of the user to retrieve.</param>
	/// <returns>An <see cref="IActionResult"/> containing the user's details if found.</returns>
	[HttpGet("{id}")]
	public async Task<IActionResult> GetUserById(Guid id)
	{
		var result = await _userService.GetUserByIdAsync(id);
		return Ok(result);
	}

	/// <summary>
	/// Retrieves a list of all registered users.
	/// </summary>
	/// <returns>
	/// An <see cref="IActionResult"/> containing a list of all users.
	/// Returns 500 Internal Server Error if an exception occurs.
	/// </returns>
	[HttpGet("Users")]
	public async Task<IActionResult> GetAll()
	{
		try
		{
			var users = await _userService.GetAllUsersAsync();
			return Ok(users);
		}
		catch (Exception ex)
		{
			return StatusCode(500, new { Error = $"Internal server error: {ex.Message}" });
		}
	}
}