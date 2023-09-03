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
    }
}
