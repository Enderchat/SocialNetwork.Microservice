namespace SocialNetwork.Contracts.Responses;

public class FriendshipResponse
{
	public Guid UserId { get; set; }
	public Guid FriendId { get; set; }
}