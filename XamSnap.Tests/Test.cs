using System;
using XamSnap.Models;

namespace XamSnap.Tests
{
	public static class Test
	{
		public static void SetUp()
		{
			ServiceContainer.Register<IWebService>(() => new FakeWebService { SleepDuration = 0 });
			ServiceContainer.Register<ISettings>(() => new FakeSettings());
		}
	}
}

