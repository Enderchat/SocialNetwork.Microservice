using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Domain.Exceptions;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocialNetwork.Api.Middleware
{
	/// <summary>
	/// Middleware for global exception handling in the application.
	/// Catches exceptions during HTTP request processing and returns structured JSON error responses.
	/// </summary>
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;

		/// <summary>
		/// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
		/// </summary>
		/// <param name="next">The next middleware in the pipeline.</param>
		public ExceptionMiddleware(RequestDelegate next) => _next = next;

		/// <summary>
		/// Invokes the middleware to handle exceptions.
		/// </summary>
		/// <param name="context">The HTTP context.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (BaseException ex)
			{
				// Handle custom domain exceptions
				await HandleExceptionAsync(context, ex);
			}
			catch (Exception ex)
			{
				// Handle unexpected general exceptions
				await HandleExceptionAsync(context, new InternalServerErrorException(ex.Message));
			}
		}

		/// <summary>
		/// Handles exceptions by returning appropriate HTTP status codes and JSON error responses.
		/// </summary>
		/// <param name="context">The HTTP context.</param>
		/// <param name="exception">The exception that occurred.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous response writing.</returns>
		private static Task HandleExceptionAsync(HttpContext context, BaseException exception)
		{
			var code = HttpStatusCode.InternalServerError; // Default fallback

			string result = JsonSerializer.Serialize(new
			{
				error = "Произошла необработанная ошибка"
			});

			switch (exception)
			{
				case UserNotFoundException userNotFound:
					// Return 404 Not Found with user-specific info
					code = HttpStatusCode.NotFound;
					result = JsonSerializer.Serialize(new
					{
						error = "Пользователь не найден",
						userId = userNotFound.UserId,
						message = userNotFound.Message
					});
					break;

				case FriendshipExistsException friendExists:
					// Return 409 Conflict when friendship already exists
					code = HttpStatusCode.Conflict;
					result = JsonSerializer.Serialize(new
					{
						error = "Дружба уже существует",
						userId = friendExists.UserId,
						friendId = friendExists.FriendId,
						message = friendExists.Message
					});
					break;

				case ValidationException validationEx:
					// Return 400 Bad Request with validation errors
					code = HttpStatusCode.BadRequest;
					result = JsonSerializer.Serialize(new
					{
						error = "Ошибка валидации",
						errors = validationEx.Errors
					});
					break;

				default:
					// Fallback for any other custom exceptions not explicitly handled
					code = HttpStatusCode.InternalServerError;
					result = JsonSerializer.Serialize(new
					{
						error = exception.Message
					});
					break;
			}

			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)code;

			return context.Response.WriteAsync(result);
		}

		/// <summary>
		/// Helper method to write error response to the HTTP context.
		/// </summary>
		/// <param name="context">The HTTP context.</param>
		/// <param name="code">The HTTP status code to set.</param>
		/// <param name="result">The error message object to serialize and send.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous response writing.</returns>
		private static Task WriteResponse(HttpContext context, HttpStatusCode code, object result)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)code;
			return context.Response.WriteAsync(JsonSerializer.Serialize(result));
		}
	}
}