using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Contracts.Requests;

/// <summary>
/// Represents a request to send a message from one user to another.
/// Contains validation attributes to ensure data integrity.
/// </summary>
public class SendMessageRequest
{
	/// <summary>
	/// Gets or sets the unique identifier of the user sending the message.
	/// This field is required.
	/// </summary>
	[Required(ErrorMessage = "SenderId is required.")]
	public Guid SenderId { get; set; }

	/// <summary>
	/// Gets or sets the unique identifier of the user receiving the message.
	/// This field is required.
	/// </summary>
	[Required(ErrorMessage = "ReceiverId is required.")]
	public Guid ReceiverId { get; set; }

	/// <summary>
	/// Gets or sets the content of the message being sent.
	/// The content must not be empty and must not exceed 500 characters.
	/// </summary>
	[Required(ErrorMessage = "Content is required.")]
	[MinLength(1, ErrorMessage = "Message content cannot be empty.")]
	[MaxLength(500, ErrorMessage = "Message content must not exceed 500 characters.")]
	public string Content { get; set; } = string.Empty;
}