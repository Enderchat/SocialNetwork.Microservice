using Microsoft.EntityFrameworkCore;
using SocialNetwork.Infrastructure.Entities;

namespace SocialNetwork.Infrastructure.Data;

/// <summary>
/// Represents the database context for the social network application.
/// Manages entity sets and relationships for users, messages, and friendships.
/// </summary>
public class AppDbContext : DbContext
{
	/// <summary>
	/// Gets or sets the DbSet of <see cref="UserDb"/> entities.
	/// Represents all users in the system.
	/// </summary>
	public DbSet<UserDb> Users { get; set; }

	/// <summary>
	/// Gets or sets the DbSet of <see cref="MessageDb"/> entities.
	/// Represents all messages exchanged between users.
	/// </summary>
	public DbSet<MessageDb> Messages { get; set; }

	/// <summary>
	/// Gets or sets the DbSet of <see cref="FriendshipDb"/> entities.
	/// Represents friendship relationships between users.
	/// </summary>
	public DbSet<FriendshipDb> Friendships { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="AppDbContext"/> class.
	/// </summary>
	/// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	/// <summary>
	/// Configures the model that was discovered by convention.
	/// This method is called after the model has been created but before it is finalized.
	/// Sets up relationships and keys for the domain model.
	/// </summary>
	/// <param name="modelBuilder">Provides a simple API surface for configuring a <see cref="IMutableModel"/>.</param>
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// Конфигурация Friendship (Many-to-Many)
		modelBuilder.Entity<FriendshipDb>()
			.HasKey(f => new { f.UserId, f.FriendId });

		modelBuilder.Entity<FriendshipDb>()
			.HasOne(f => f.User)
			.WithMany(u => u.Friendships)
			.HasForeignKey(f => f.UserId);

		modelBuilder.Entity<FriendshipDb>()
			.HasOne(f => f.Friend)
			.WithMany()
			.HasForeignKey(f => f.FriendId);

		// Конфигурация Message (1-to-Many)
		modelBuilder.Entity<MessageDb>()
			.HasOne(m => m.Sender)
			.WithMany(u => u.SentMessages)
			.HasForeignKey(m => m.SenderId);

		modelBuilder.Entity<MessageDb>()
			.HasOne(m => m.Receiver)
			.WithMany(u => u.ReceivedMessages)
			.HasForeignKey(m => m.ReceiverId);
	}
}