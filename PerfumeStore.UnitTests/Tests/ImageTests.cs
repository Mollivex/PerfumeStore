using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using PerfumeStore.Domain.Entities;
using PerfumeStore.Domain.Abstract;
using PerfumeStore.WebUI.Controllers;


namespace PerfumeStore.UnitTests
{
    [TestClass]
    public class ImageTests
    {
        [TestMethod]
        public void Can_Retrieve_Image_Data()
        {
            // Arrange - creating Perfume object with image data
            Perfume perfume = new Perfume
            {
                PerfumeId = 2,
                PerfumeName = "Fragrance2",
                ImageData = new byte[] { },
                ImageMimeType = "image/png"
            };

            // Arrange - creating simulated repostory
            Mock<IPerfumeRepository> mock = new Mock<IPerfumeRepository>();
            mock.Setup(m => m.Perfumes).Returns(new List<Perfume>
            {
                new Perfume { PerfumeId = 1, PerfumeName = "Fragrance1"},
                perfume,
                new Perfume { PerfumeId = 3, PerfumeName = "Fragrance3"},
            }.AsQueryable());

            // Arrange - creating controller
            PerfumeController controller = new PerfumeController(mock.Object);

            // Action - calling GetImage() action method
            ActionResult result = controller.GetImage(2);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(perfume.ImageMimeType, ((FileResult)result).ContentType);
        }

        [TestMethod]
        public void Cannot_Retrieve_Image_Data_For_Invalid_ID()
        {
            // Arrange - creating simulated repostory
            Mock<IPerfumeRepository> mock = new Mock<IPerfumeRepository>();
            mock.Setup(m => m.Perfumes).Returns(new List<Perfume>
            {
                new Perfume { PerfumeId = 1, PerfumeName = "Fragrance1"},
                new Perfume { PerfumeId = 2, PerfumeName = "Fragrance2"},
            }.AsQueryable());

            // Arrange - creating controller
            PerfumeController controller = new PerfumeController(mock.Object);

            // Action - calling GetImage() action method
            ActionResult result = controller.GetImage(10);

            // Assert
            Assert.IsNull(result);
        }
    }
}
