using System.Collections.Generic;

namespace SocialNetwork.Domain.Exceptions;

/// <summary>
/// Represents an exception that is thrown when one or more validation errors occur during business logic execution.
/// This exception contains a dictionary of validation errors grouped by property names.
/// </summary>
public class ValidationException : BaseException
{
	/// <summary>
	/// Gets a dictionary containing validation errors.
	/// The key is the name of the property, and the value is an array of error messages for that property.
	/// </summary>
	public IDictionary<string, string[]> Errors { get; }

	/// <summary>
	/// Initializes a new instance of the <see cref="ValidationException"/> class 
	/// with a specified collection of validation errors.
	/// </summary>
	/// <param name="errors">A dictionary containing validation errors grouped by property names.</param>
	public ValidationException(IDictionary<string, string[]> errors)
		: base("One or more validation errors occurred.")
	{
		Errors = errors;
	}
}