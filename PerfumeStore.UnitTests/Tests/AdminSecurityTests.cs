using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using Moq;
using PerfumeStore.WebUI.Controllers;
using PerfumeStore.WebUI.Infrastructure.Abstract;
using PerfumeStore.WebUI.Models;
using Ninject.Planning.Targets;

namespace PerfumeStore.UnitTests
{
    [TestClass]
    public class AdminSecurityTests
    {
        [TestMethod]
        public void Can_Login_With_Valid_Credentials()
        {
            // Arrange - creating mocked authentication provider
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "12345")).Returns(true);

            // Arrange - creating view model with valid credentials
            LoginViewModel model = new LoginViewModel
            {
                UserName = "admin",
                Password = "12345",
            };

            // Arrange - creating controller
            AccountController target = new AccountController(mock.Object);

            // Action - authentication
            ActionResult result = target.Login(model, "/MyURL");

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("/MyURL", ((RedirectResult)result).Url);
        }

        [TestMethod]
        public void Cannot_Login_With_Invalid_Credentials()
        {
            // Arrange - creating mocked authentication provider
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("badUser", "badPass")).Returns(false);

            // Arrange - creating view model with INvalid credentials
            LoginViewModel model = new LoginViewModel
            {
                UserName = "badUser",
                Password = "badPass",
            };

            // Arrange - creating controller
            AccountController target = new AccountController(mock.Object);

            // Action - authentication
            ActionResult result = target.Login(model, "/MyURL");

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        }
    }
}
