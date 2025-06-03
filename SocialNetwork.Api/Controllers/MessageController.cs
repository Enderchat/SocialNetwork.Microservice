using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Contracts.Requests;
using SocialNetwork.Contracts.Responses;
using SocialNetwork.Core.Services;
using SocialNetwork.Domain.Exceptions;
using System.Threading.Tasks;

namespace SocialNetwork.Api.Controllers;

/// <summary>
/// Controller responsible for handling message-related operations in the social network API.
/// Provides endpoints for sending messages between users.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Tags("Messages")] // Tags are used by Swagger to group endpoints
public class MessageController : ControllerBase
{
	private readonly IMessageService _messageService;

	/// <summary>
	/// Initializes a new instance of the <see cref="MessageController"/> class.
	/// </summary>
	/// <param name="messageService">The service used to handle message operations.</param>
	public MessageController(IMessageService messageService)
	{
		_messageService = messageService;
	}

	/// <summary>
	/// Sends a message from one user to another.
	/// </summary>
	/// <param name="request">The request containing sender ID, receiver ID, and message content.</param>
	/// <returns>The sent message details if successful.</returns>
	/// <response code="200">Returns the message that was successfully sent.</response>
	/// <response code="400">If the friendship does not exist or validation fails.</response>
	/// <response code="404">If either sender or receiver is not found.</response>
	/// <response code="500">If an internal server error occurs.</response>
	[HttpPost("send")]
	public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
	{
		try
		{
			var message = await _messageService.SendMessageAsync(request.SenderId, request.ReceiverId, request.Content);
			return Ok(message);
		}
		catch (UserNotFoundException ex)
		{
			return NotFound(new { Error = ex.Message });
		}
		catch (FriendshipNotFoundException ex)
		{
			return BadRequest(new { Error = ex.Message });
		}
		catch (Exception ex)
		{
			return StatusCode(500, new { Error = $"Internal server error: {ex.Message}" });
		}
	}
}