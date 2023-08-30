using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using PerfumeStore.Domain.Abstract;
using PerfumeStore.Domain.Entities;
using PerfumeStore.WebUI.Controllers;
using PerfumeStore.WebUI.Models;
using PerfumeStore.WebUI.HtmlHelpers;

namespace PerfumeStore.UnitTests
{
    [TestClass]
    public class PerfumeTests
    {
        [TestMethod]
        public void Can_Paginate()
        {
            // Arrange
            Mock<IPerfumeRepository> mock = new Mock<IPerfumeRepository>();
            mock.Setup(m => m.Perfumes).Returns(new List<Perfume>
            {
                new Perfume { PerfumeId = 1, Name = "Fragrance1" },
                new Perfume { PerfumeId = 2, Name = "Fragrance2" },
                new Perfume { PerfumeId = 3, Name = "Fragrance3" },
                new Perfume { PerfumeId = 4, Name = "Fragrance4" },
                new Perfume { PerfumeId = 5, Name = "Fragrance5" }
            });

            // Arrange - creating controller
            PerfumeController controller = new PerfumeController(mock.Object);
            controller.pageSize = 3;

            // Act
            PerfumeListViewModel result
                = (PerfumeListViewModel)controller.List(2).Model;

            // Assert
            List<Perfume> perfumes = result.Perfumes.ToList();
            Assert.IsTrue(perfumes.Count == 2);
            Assert.AreEqual(perfumes[0].Name, "Fragrance4");
            Assert.AreEqual(perfumes[1].Name, "Fragrance5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            // Arrange
            HtmlHelper myHelper = null;

            // Arrange - creating PagingInfo object
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // Arrange - delegate setting with lambda-expression
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Assert
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>" +
                            @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>" +
                            @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                            result.ToString());
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            // Arrange - creating simulated repository
            Mock<IPerfumeRepository> mock = new Mock<IPerfumeRepository>();
            mock.Setup(m => m.Perfumes).Returns(new List<Perfume>
            {
                new Perfume { PerfumeId = 1, Name = "Fragrance1" },
                new Perfume { PerfumeId = 2, Name = "Fragrance2" },
                new Perfume { PerfumeId = 3, Name = "Fragrance3" },
                new Perfume { PerfumeId = 4, Name = "Fragrance4" },
                new Perfume { PerfumeId = 5, Name = "Fragrance5" }
            });

            // Arrange - creating controller
            PerfumeController controller = new PerfumeController(mock.Object);
            controller.pageSize = 3;

            // Act
            PerfumeListViewModel result
                = (PerfumeListViewModel)controller.List(2).Model;

            // Assert
            PagingInfo pageinfo = result.PagingInfo;
            Assert.AreEqual(pageinfo.CurrentPage, 2);
            Assert.AreEqual(pageinfo.ItemsPerPage, 3);
            Assert.AreEqual(pageinfo.TotalItems, 5);
            Assert.AreEqual(pageinfo.TotalPages, 2);
        }
    }
}
