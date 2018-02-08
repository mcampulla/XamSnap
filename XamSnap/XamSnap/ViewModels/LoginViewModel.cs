using System;
using System.Threading.Tasks;
using XamSnap.Models;

namespace XamSnap.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
        readonly INotificationService notificationService = ServiceContainer.Resolve<INotificationService>();
        public string Username { get; set; }
        public string Password { get; set; }

		public async Task Login()
		{
			if (string.IsNullOrEmpty(Username))
				throw new Exception("Username is blank.");

			if (string.IsNullOrEmpty(Password))
				throw new Exception("Password is blank.");

			IsBusy = true;
			try
			{
				settings.User = await service.Login(Username, Password);
				settings.Save();
                notificationService.Start(Username);
            }
			finally
			{
				IsBusy = false;
			}
		}
	}
}

