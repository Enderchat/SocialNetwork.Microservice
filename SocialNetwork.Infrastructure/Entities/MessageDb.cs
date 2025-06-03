namespace SocialNetwork.Infrastructure.Entities;

/// <summary>
/// Represents a message stored in the database.
/// Contains properties for message content, sender, receiver, and timestamp.
/// </summary>
public class MessageDb
{
	/// <summary>
	/// Gets or sets the unique identifier of the message.
	/// Defaults to a new GUID if not explicitly set.
	/// </summary>
	public Guid Id { get; set; } = Guid.NewGuid();

	/// <summary>
	/// Gets or sets the unique identifier of the user who sent the message.
	/// This property is required and must not be empty.
	/// </summary>
	public Guid SenderId { get; set; }

	/// <summary>
	/// Gets or sets the unique identifier of the user who received the message.
	/// This property is required and must not be empty.
	/// </summary>
	public Guid ReceiverId { get; set; }

	/// <summary>
	/// Gets or sets the content of the message.
	/// Must not exceed 500 characters and defaults to an empty string if not provided.
	/// </summary>
	public string Content { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the date and time when the message was sent.
	/// Defaults to the current UTC time if not explicitly set.
	/// </summary>
	public DateTime SentAt { get; set; }

	/// <summary>
	/// Gets or sets the navigation property for the sender of the message.
	/// This allows Entity Framework Core to navigate from the message to the associated sender.
	/// </summary>
	public UserDb? Sender { get; set; }

	/// <summary>
	/// Gets or sets the navigation property for the receiver of the message.
	/// This allows Entity Framework Core to navigate from the message to the associated receiver.
	/// </summary>
	public UserDb? Receiver { get; set; }
}