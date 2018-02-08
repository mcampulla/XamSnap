using System;
using System.Threading.Tasks;
using XamSnap.ViewModels;
using XamSnap.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XamSnap.Tests
{
    [TestClass]
    public class LoginViewModelTests
	{
		LoginViewModel loginViewModel;
		ISettings settings;

        [TestInitialize]
        public void SetUp()
        {
            Test.SetUp();

            settings = ServiceContainer.Resolve<ISettings>();
            loginViewModel = new LoginViewModel();
        }

        [TestMethod]
		public async Task LoginSuccessfully()
		{
			loginViewModel.Username = "testuser";
			loginViewModel.Password = "password";

			await loginViewModel.Login();

            Assert.IsNotNull(settings.User);
		}

		//[TestMethod, ExpectedException(typeof(Exception), ExpectedMessage = "Username is blank.")]
        [TestMethod, ExpectedException(typeof(Exception))]
        public async Task LoginWithNoUsernameOrPassword()
		{
			//Throws an exception
			await loginViewModel.Login();
		}
	}
}

