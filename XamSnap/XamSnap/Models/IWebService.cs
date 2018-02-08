using System;
using System.Threading.Tasks;

namespace XamSnap.Models
{
	public interface IWebService
	{
		Task<User> Login(string username, string password);

		Task<User> Register(User user);

		Task<User[]> GetFriends(string userId);

		Task<User> AddFriend(string userId, string username);

		Task<Conversation[]> GetConversations(string userId);

		Task<SnapMessage[]> GetMessages(string conversationId);

		Task<SnapMessage> SendMessage(SnapMessage message);
	}
}

