namespace SocialNetwork.Domain.Exceptions;

/// <summary>
/// Represents the base class for all custom exceptions in the social network domain.
/// This abstract class serves as a common base type for domain-specific exception handling.
/// </summary>
public abstract class BaseException : Exception
{
	/// <summary>
	/// Initializes a new instance of the <see cref="BaseException"/> class with a specified error message.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	protected BaseException(string message) : base(message)
	{
		// No implementation needed here — this is just a base class for inheritance
	}
}