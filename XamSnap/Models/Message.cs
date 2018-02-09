using System;

namespace XamSnap.Models
{
	public class SnapMessage
	{
		public string Id { get; set; }

		public string ConversationId { get; set; }

		public string UserId { get; set; } 

		public string Username { get; set; }

		public string Text { get; set; }

        public DateTime MessageDate { get; set; }
    }
}

