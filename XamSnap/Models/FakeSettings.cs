using System;

namespace XamSnap.Models
{
	public class FakeSettings : ISettings
	{
		public User User { get; set; }

		public void Save() { }
	}
}

