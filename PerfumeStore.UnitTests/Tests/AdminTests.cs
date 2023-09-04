using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PerfumeStore.Domain.Abstract;
using PerfumeStore.Domain.Entities;
using PerfumeStore.WebUI.Controllers;

namespace PerfumeStore.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contatins_All_Games()
        {
            // Arrange - creating simulated repository
            Mock<IPerfumeRepository> mock = new Mock<IPerfumeRepository>();
            mock.Setup(m => m.Perfumes).Returns(new List<Perfume>
            {
                new Perfume { PerfumeId = 1, PerfumeName = "Fragrance1"},
                new Perfume { PerfumeId = 2, PerfumeName = "Fragrance2"},
                new Perfume { PerfumeId = 3, PerfumeName = "Fragrance3"},
                new Perfume { PerfumeId = 4, PerfumeName = "Fragrance4"},
                new Perfume { PerfumeId = 5, PerfumeName = "Fragrance5"}
            });

            // Arrange - creating controller
            AdminController controller = new AdminController(mock.Object);

            // Action
            List<Perfume> result = ((IEnumerable<Perfume>)controller.Index()
                .ViewData.Model).ToList();

            // Assert
            Assert.AreEqual(result.Count(), 5);
            Assert.AreEqual("Fragrance1", result[0].PerfumeName);
            Assert.AreEqual("Fragrance2", result[1].PerfumeName);
            Assert.AreEqual("Fragrance3", result[2].PerfumeName);
        }

        [TestMethod]
        public void Can_Edit_Perfume()
        {
            // Arrange - creating simulated repository
            Mock<IPerfumeRepository> mock = new Mock<IPerfumeRepository>();
            mock.Setup(m => m.Perfumes).Returns(new List<Perfume>
            {
                new Perfume { PerfumeId = 1, PerfumeName = "Fragrance1"},
                new Perfume { PerfumeId = 2, PerfumeName = "Fragrance2"},
                new Perfume { PerfumeId = 3, PerfumeName = "Fragrance3"},
                new Perfume { PerfumeId = 4, PerfumeName = "Fragrance4"},
                new Perfume { PerfumeId = 5, PerfumeName = "Fragrance5"}
            });

            // Arrange - creating controller
            AdminController controller = new AdminController(mock.Object);

            // Action
            Perfume perfume1 = controller.Edit(1).ViewData.Model as Perfume;
            Perfume perfume2 = controller.Edit(2).ViewData.Model as Perfume;
            Perfume perfume3 = controller.Edit(3).ViewData.Model as Perfume;

            // Assert
            Assert.AreEqual(1, perfume1.PerfumeId);
            Assert.AreEqual(2, perfume2.PerfumeId);
            Assert.AreEqual(3, perfume3.PerfumeId);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Perfume()
        {
            // Arrange - creating simulated repository
            Mock<IPerfumeRepository> mock = new Mock<IPerfumeRepository>();
            mock.Setup(m => m.Perfumes).Returns(new List<Perfume>
            {
                new Perfume { PerfumeId = 1, PerfumeName = "Fragrance1"},
                new Perfume { PerfumeId = 2, PerfumeName = "Fragrance2"},
                new Perfume { PerfumeId = 3, PerfumeName = "Fragrance3"},
                new Perfume { PerfumeId = 4, PerfumeName = "Fragrance4"},
                new Perfume { PerfumeId = 5, PerfumeName = "Fragrance5"}
            });

            // Arrange - creating controller
            AdminController controller = new AdminController(mock.Object);

            // Action
            Perfume perfume = controller.Edit(6).ViewData.Model as Perfume;

            // Assert
        }

        [TestMethod]
        public void Can_Svae_Valid_Changes()
        {
            // Arrange - creating simulated repository
            Mock<IPerfumeRepository> mock = new Mock<IPerfumeRepository>();

            // Arrange - creating controller
            AdminController controller = new AdminController(mock.Object);

            // Arrange - creating Perfume object
            Perfume perfume = new Perfume { PerfumeName = "Test" };

            // Action - trying to save item
            ActionResult result = controller.Edit(perfume);

            // Assert - checking that repository has been accessed
            mock.Verify(m => m.SavePerfume(perfume));

            // Assert - checking method result type
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            // Arrange - creating simulated repository
            Mock<IPerfumeRepository> mock = new Mock<IPerfumeRepository>();

            // Arrange - creating controller
            AdminController controller = new AdminController(mock.Object);

            // Arrange - creating Perfume object
            Perfume perfume = new Perfume { PerfumeName = "Test" };

            // Arrange - adding error in the model state
            controller.ModelState.AddModelError("error", "error");

            // Action - trying to save item
            ActionResult result = controller.Edit(perfume);

            // Assert - checking that repository has NOT been accessed
            mock.Verify(m => m.SavePerfume(It.IsAny<Perfume>()), Times.Never());

            // Assert - checking method result type
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
