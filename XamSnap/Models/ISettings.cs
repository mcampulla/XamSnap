using System;

namespace XamSnap.Models
{
	public interface ISettings
	{
		User User { get; set; }

		void Save();
	}
}

