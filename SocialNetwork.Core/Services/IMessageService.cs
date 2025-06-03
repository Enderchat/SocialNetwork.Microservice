using SocialNetwork.Contracts.Requests;
using SocialNetwork.Contracts.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Services
{
	/// <summary>
	/// Defines the contract for message-related operations in the social network application.
	/// This interface provides methods to send messages and retrieve message history between users.
	/// </summary>
	public interface IMessageService
	{
		/// <summary>
		/// Sends a message from one user to another asynchronously.
		/// </summary>
		/// <param name="senderId">The unique identifier of the user sending the message.</param>
		/// <param name="receiverId">The unique identifier of the user receiving the message.</param>
		/// <param name="content">The content of the message being sent.</param>
		/// <returns>
		/// A task that represents the asynchronous operation. 
		/// The task result contains a <see cref="MessageResponse"/> object representing the sent message.
		/// </returns>
		Task<MessageResponse> SendMessageAsync(Guid senderId, Guid receiverId, string content);

		/// <summary>
		/// Retrieves all messages exchanged between two users asynchronously.
		/// </summary>
		/// <param name="id1">The ID of the first user.</param>
		/// <param name="id2">The ID of the second user.</param>
		/// <returns>
		/// A task that represents the asynchronous operation. 
		/// The task result contains a collection of <see cref="MessageResponse"/> objects 
		/// representing the messages exchanged between the two users.
		/// </returns>
		Task<IEnumerable<MessageResponse>> GetMessagesBetweenAsync(Guid id1, Guid id2);
	}
}