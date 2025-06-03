using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Contracts.Requests;
using SocialNetwork.Contracts.Responses;
using SocialNetwork.Core.Services;
using SocialNetwork.Domain.Exceptions;
using System.Threading.Tasks;

namespace SocialNetwork.Api.Controllers
{
	/// <summary>
	/// Controller for managing user friendships in the social network.
	/// Provides endpoints for adding friends, checking friendship status, and retrieving friend lists.
	/// </summary>
	[ApiController]
	[Route("api/friends")]
	public class FriendshipController : ControllerBase
	{
		private readonly IFriendshipService _friendshipService;

		/// <summary>
		/// Initializes a new instance of the <see cref="FriendshipController"/> class.
		/// </summary>
		/// <param name="friendshipService">The service used to perform friendship operations.</param>
		public FriendshipController(IFriendshipService friendshipService)
		{
			_friendshipService = friendshipService;
		}

		/// <summary>
		/// Adds a new friend relationship between two users.
		/// </summary>
		/// <param name="request">An object containing the IDs of the user and the friend to add.</param>
		/// <returns>
		/// An <see cref="IActionResult"/> that represents the result of the asynchronous operation.
		/// Returns 200 OK if successful, 404 Not Found if either user does not exist,
		/// 409 Conflict if the friendship already exists, or 500 Internal Server Error on failure.
		/// </returns>
		[HttpPost("add")]
		public async Task<IActionResult> AddFriend([FromBody] AddFriendRequest request)
		{
			try
			{
				await _friendshipService.AddFriendAsync(request.UserId, request.FriendId);
				return Ok(new { Message = "Друг успешно добавлен." });
			}
			catch (UserNotFoundException ex)
			{
				return NotFound(new { Error = ex.Message });
			}
			catch (FriendshipExistsException ex)
			{
				return Conflict(new { Error = ex.Message });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { Error = $"Internal server error: {ex.Message}" });
			}
		}

		/// <summary>
		/// Retrieves a list of friends for the specified user.
		/// </summary>
		/// <param name="userId">The ID of the user whose friends are being retrieved.</param>
		/// <returns>
		/// An <see cref="IActionResult"/> containing an OK response with the list of friends.
		/// </returns>
		[HttpGet("{userId}/friends")]
		public async Task<IActionResult> GetFriends(Guid userId)
		{
			var friends = await _friendshipService.GetFriendsAsync(userId);
			return Ok(friends);
		}

		/// <summary>
		/// Checks whether two users are friends.
		/// </summary>
		/// <param name="userId">The ID of the first user.</param>
		/// <param name="friendId">The ID of the second user to check against.</param>
		/// <returns>
		/// Returns 200 OK with {"Friends": true} if they are friends.
		/// Throws a <see cref="FriendshipNotFoundException"/> if no friendship exists.
		/// </returns>
		[HttpGet("{userId}/friends/{friendId}")]
		public async Task<IActionResult> AreFriends(Guid userId, Guid friendId)
		{
			if (!await _friendshipService.AreFriendsAsync(userId, friendId))
			{
				throw new FriendshipNotFoundException(userId, friendId);
			}

			return Ok(new { Friends = true });
		}
	}
}