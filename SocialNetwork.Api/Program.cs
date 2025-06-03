using Microsoft.EntityFrameworkCore;
using SocialNetwork.Api.Middleware;
using SocialNetwork.Api.Services;
using SocialNetwork.Core.Repositories;
using SocialNetwork.Core.Services;
using SocialNetwork.Domain.Mappings;
using SocialNetwork.Infrastructure.Data;
using SocialNetwork.Infrastructure.Repositories;
using SocialNetwork.Infrastructure.Services;

// Entry point of the application
var builder = WebApplication.CreateBuilder(args);

// Configure database context using SQLite connection string from appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories in the dependency injection container
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IFriendshipRepository, FriendshipRepository>();

// Register services in the dependency injection container
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IFriendshipService, FriendshipService>();

// Add controllers and Swagger for API documentation
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register AutoMapper with MappingProfile to handle object-to-object mapping
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Apply EF Core migrations automatically at startup
using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	await dbContext.Database.MigrateAsync();
}

// Use custom middleware for global exception handling
app.UseMiddleware<ExceptionMiddleware>();

// Enable Swagger UI only in Development environment
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

// Use HTTPS redirection and authorization middleware
app.UseHttpsRedirection();
app.UseAuthorization();

// Map controller routes
app.MapControllers();

// Run the application asynchronously
await app.RunAsync();