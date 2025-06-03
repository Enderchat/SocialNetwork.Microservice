using AutoMapper;
using SocialNetwork.Contracts.Responses;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Infrastructure.Entities;

namespace SocialNetwork.Domain.Mappings
{
	/// <summary>
	/// Configuration class for AutoMapper mappings between domain models and data models.
	/// Defines bidirectional mappings for users, messages, friendships, and responses.
	/// </summary>
	public class MappingProfile : Profile
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MappingProfile"/> class.
		/// Configures all necessary mappings between domain and infrastructure entities.
		/// </summary>
		public MappingProfile()
		{
			// User ↔ UserDb mapping
			CreateMap<User, UserDb>().ReverseMap();

			// Message ↔ MessageDb mapping
			CreateMap<Message, MessageDb>().ReverseMap();

			// Message → MessageResponse mapping
			CreateMap<Message, MessageResponse>();

			// Friendship ↔ FriendshipDb mapping
			CreateMap<Friendship, FriendshipDb>().ReverseMap();

			// UserDb → UserResponse mapping
			CreateMap<UserDb, UserResponse>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

			// FriendshipDb → UserResponse mapping
			// Used to get friend details from friendship records
			CreateMap<FriendshipDb, UserResponse>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.FriendId))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Friend?.Name));
		}
	}
}