using FluentValidation;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Domain.Validations;

/// <summary>
/// Validator for the <see cref="User"/> entity.
/// Ensures that user data meets required validation rules before being processed.
/// </summary>
public class UserValidator : AbstractValidator<User>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="UserValidator"/> class.
	/// Sets up validation rules for user properties such as name.
	/// </summary>
	public UserValidator()
	{
		// Validate user name
		RuleFor(u => u.Name)
			.NotEmpty().WithMessage("Name is required.")
			.MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
	}
}