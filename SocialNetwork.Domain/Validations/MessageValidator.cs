using FluentValidation;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Domain.Validations;

/// <summary>
/// Validator for the <see cref="Message"/> entity.
/// Ensures that message data meets required validation rules before being processed.
/// </summary>
public class MessageValidator : AbstractValidator<Message>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="MessageValidator"/> class.
	/// Sets up validation rules for message content, sender, and receiver.
	/// </summary>
	public MessageValidator()
	{
		// Validate message content
		RuleFor(m => m.Content)
			.NotEmpty().WithMessage("Message content is required.")
			.MaximumLength(500).WithMessage("Message must not exceed 500 characters.");

		// Validate sender ID
		RuleFor(m => m.SenderId)
			.NotEqual(Guid.Empty).WithMessage("SenderId is invalid.");

		// Validate receiver ID
		RuleFor(m => m.ReceiverId)
			.NotEqual(Guid.Empty).WithMessage("ReceiverId is invalid.");
	}
}