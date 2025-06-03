using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Repositories;
using SocialNetwork.Infrastructure.Data;
using SocialNetwork.Infrastructure.Entities;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Repositories
{
	/// <summary>
	/// Implementation of the <see cref="IFriendshipRepository"/> interface.
	/// Provides data access logic for friendship-related operations in the database.
	/// </summary>
	public class FriendshipRepository : IFriendshipRepository
	{
		private readonly AppDbContext _context;

		/// <summary>
		/// Initializes a new instance of the <see cref="FriendshipRepository"/> class.
		/// </summary>
		/// <param name="context">The database context to use for data operations.</param>
		public FriendshipRepository(AppDbContext context) => _context = context;

		/// <summary>
		/// Adds a new friendship relationship to the database asynchronously.
		/// </summary>
		/// <param name="friendship">The friendship entity to add.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		public async Task AddAsync(FriendshipDb friendship)
		{
			await _context.Friendships.AddAsync(friendship);
		}

		/// <summary>
		/// Removes a friendship relationship between two users asynchronously.
		/// </summary>
		/// <param name="userId">The ID of the user whose friendship is being removed.</param>
		/// <param name="friendId">The ID of the friend to remove.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		public async Task RemoveAsync(Guid userId, Guid friendId)
		{
			var friendship = await _context.Friendships.FindAsync(userId, friendId);
			if (friendship != null)
			{
				_context.Friendships.Remove(friendship);
			}
		}

		/// <summary>
		/// Checks whether a friendship exists between two users asynchronously.
		/// </summary>
		/// <param name="userId">The ID of the first user.</param>
		/// <param name="friendId">The ID of the second user.</param>
		/// <returns>
		/// A task that represents the asynchronous operation. 
		/// The task result contains a boolean indicating whether the users are friends.
		/// </returns>
		public async Task<bool> AreFriendsAsync(Guid userId, Guid friendId)
		{
			return await _context.Friendships.AnyAsync(f => f.UserId == userId && f.FriendId == friendId);
		}

		/// <summary>
		/// Retrieves all friendships associated with a specific user asynchronously.
		/// Includes related friend entities in the results.
		/// </summary>
		/// <param name="userId">The ID of the user whose friendships are being retrieved.</param>
		/// <returns>
		/// A task that represents the asynchronous operation. 
		/// The task result contains a collection of friendship entities.
		/// </returns>
		public async Task<IEnumerable<FriendshipDb>> GetFriendsAsync(Guid userId)
		{
			return await _context.Friendships
				.Where(f => f.UserId == userId)
				.Include(f => f.Friend)
				.ToListAsync();
		}

		/// <summary>
		/// Saves all changes made in this repository to the database asynchronously.
		/// </summary>
		/// <returns>A task that represents the asynchronous save operation.</returns>
		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}