using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Domain.Entities;

/// <summary>
/// Represents a message sent from one user to another in the social network.
/// </summary>
public class Message
{
	/// <summary>
	/// Gets or sets the unique identifier of the message.
	/// Defaults to a new GUID if not explicitly set.
	/// </summary>
	public Guid Id { get; set; } = Guid.NewGuid();

	/// <summary>
	/// Gets or sets the unique identifier of the user who sent the message.
	/// This field is required.
	/// </summary>
	[Required]
	public Guid SenderId { get; set; }

	/// <summary>
	/// Gets or sets the unique identifier of the user who received the message.
	/// This field is required.
	/// </summary>
	[Required]
	public Guid ReceiverId { get; set; }

	/// <summary>
	/// Gets or sets the content of the message.
	/// Must not exceed 500 characters and cannot be null.
	/// Defaults to an empty string if not explicitly set.
	/// </summary>
	[Required]
	[MaxLength(500)]
	public string Content { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the date and time when the message was sent.
	/// Defaults to the current UTC time if not explicitly set.
	/// </summary>
	public DateTime SentAt { get; set; } = DateTime.UtcNow;

	/// <summary>
	/// Gets or sets the user entity representing the sender of the message.
	/// May be null if not loaded.
	/// </summary>
	public User? Sender { get; set; }

	/// <summary>
	/// Gets or sets the user entity representing the receiver of the message.
	/// May be null if not loaded.
	/// </summary>
	public User? Receiver { get; set; }
}