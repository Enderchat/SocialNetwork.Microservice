using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SocialNetwork.Infrastructure.Data.MigrationTools
{
	/// <summary>
	/// Factory class used by EF Core tools at design time to create instances of <see cref="AppDbContext"/>.
	/// Required for running migrations from the command line or using the Package Manager Console.
	/// </summary>
	public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
	{
		/// <summary>
		/// Creates a new instance of <see cref="AppDbContext"/> using configuration from appsettings.json.
		/// This method is used by EF Core tools when applying or creating migrations.
		/// </summary>
		/// <param name="args">Command-line arguments passed to the tool.</param>
		/// <returns>An instance of <see cref="AppDbContext"/>.</returns>
		public AppDbContext CreateDbContext(string[] args)
		{
			// Build configuration to access connection strings and settings
			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			// Configure DbContext options
			var builder = new DbContextOptionsBuilder<AppDbContext>();
			var connectionString = configuration.GetConnectionString("DefaultConnection");

			// Use SQLite as the database provider
			builder.UseSqlite(connectionString);

			// Return a new instance of AppDbContext with configured options
			return new AppDbContext(builder.Options);
		}
	}
}