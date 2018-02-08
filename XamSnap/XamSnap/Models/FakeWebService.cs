using System;
using System.Threading.Tasks;

namespace XamSnap.Models
{
	public class FakeWebService : IWebService
	{
		public int SleepDuration { get; set; }

		public FakeWebService()
		{
			SleepDuration = 300;
		}

		private Task Sleep()
		{
			return Task.Delay(SleepDuration);
		}

		public async Task<User> Login(string username, string password)
		{
			await Sleep();

			return new User { Id = "1", Username = username };
		}

		public async Task<User> Register(User user)
		{
			await Sleep();

			return user;
		}

		public async Task<User[]> GetFriends(string userId)
		{
			await Sleep();

			return new[]
			{
				new User { Id = "2", Username = "bobama" },
				new User { Id = "3", Username = "bobloblaw" },
				new User { Id = "4", Username = "gmichael" },
			};
		}

		public async Task<User> AddFriend(string userId, string username)
		{
			await Sleep();

			return new User { Id = "5", Username = username };
		}

		public async Task<Conversation[]> GetConversations(string userId)
		{
			await Sleep();

			return new[]
			{
				new Conversation { Id = "1", UserId = "2", Username="bobama" },
				new Conversation { Id = "1", UserId = "3", Username="bobloblaw" },
                new Conversation { Id = "1", UserId = "4", Username="gmichael" },	
			};
		}

		public async Task<SnapMessage[]> GetMessages(string conversationId)
		{
			await Sleep();

			return new[]
			{
				new SnapMessage
                {
					Id = "1",
					ConversationId = conversationId,
					UserId = "2",
					Text = "Hey",
				},
				new SnapMessage
                {
					Id = "2",
					ConversationId = conversationId,
					UserId = "1",
					Text = "What's Up?",
				},
				new SnapMessage
                {
					Id = "3",
					ConversationId = conversationId,
					UserId = "2",
					Text = "Have you seen that new movie?",
				},
				new SnapMessage
                {
					Id = "4",
					ConversationId = conversationId,
					UserId = "1",
					Text = "It's great!",
				},
			};
		}

		public async Task<SnapMessage> SendMessage(SnapMessage message)
		{
			await Sleep();

			return message;
		}
	}
}
