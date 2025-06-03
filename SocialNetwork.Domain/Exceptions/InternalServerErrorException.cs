namespace SocialNetwork.Domain.Exceptions;

/// <summary>
/// Represents an exception that is thrown when an unexpected or internal server error occurs.
/// This exception typically indicates a problem within the application's logic or infrastructure.
/// </summary>
public class InternalServerErrorException : BaseException
{
	/// <summary>
	/// Initializes a new instance of the <see cref="InternalServerErrorException"/> class 
	/// with a specified error message.
	/// </summary>
	/// <param name="message">The message that describes the internal server error.</param>
	public InternalServerErrorException(string message) : base(message)
	{
		// No additional implementation required — message is passed to base constructor
	}
}