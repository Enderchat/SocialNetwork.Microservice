using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Api.Services;
using SocialNetwork.Core.Repositories;
using SocialNetwork.Core.Services;
using SocialNetwork.Infrastructure.Repositories;
using SocialNetwork.Infrastructure.Services;

namespace SocialNetwork.Api.Extensions
{
	/// <summary>
	/// Provides extension methods for configuring services in the dependency injection container.
	/// </summary>
	public static class ServiceExtensions
	{
		/// <summary>
		/// Registers all core services and infrastructure implementations required by the application.
		/// This includes repositories and business logic services.
		/// </summary>
		/// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
		public static void ConfigureServices(this IServiceCollection services)
		{
			// Register repositories
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IMessageRepository, MessageRepository>();
			services.AddScoped<IFriendshipRepository, FriendshipRepository>();

			// Register services
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IMessageService, MessageService>();
			services.AddScoped<IFriendshipService, FriendshipService>();
		}
	}
}