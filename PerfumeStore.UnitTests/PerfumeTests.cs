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
                new Perfume { PerfumeId = 1, PerfumeName = "Fragrance1" },
                new Perfume { PerfumeId = 2, PerfumeName = "Fragrance2" },
                new Perfume { PerfumeId = 3, PerfumeName = "Fragrance3" },
                new Perfume { PerfumeId = 4, PerfumeName = "Fragrance4" },
                new Perfume { PerfumeId = 5, PerfumeName = "Fragrance5" }
            });

            // Arrange - creating controller
            PerfumeController controller = new PerfumeController(mock.Object);
            controller.pageSize = 3;

            // Act
            PerfumeListViewModel result
                = (PerfumeListViewModel)controller.List(null, null, null, null, null, null, 2).Model;

            // Assert
            List<Perfume> perfumes = result.Perfumes.ToList();
            Assert.IsTrue(perfumes.Count == 2);
            Assert.AreEqual(perfumes[0].PerfumeName, "Fragrance4");
            Assert.AreEqual(perfumes[1].PerfumeName, "Fragrance5");
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
                new Perfume { PerfumeId = 1, PerfumeName = "Fragrance1" },
                new Perfume { PerfumeId = 2, PerfumeName = "Fragrance2" },
                new Perfume { PerfumeId = 3, PerfumeName = "Fragrance3" },
                new Perfume { PerfumeId = 4, PerfumeName = "Fragrance4" },
                new Perfume { PerfumeId = 5, PerfumeName = "Fragrance5" }
            });

            // Arrange - creating controller
            PerfumeController controller = new PerfumeController(mock.Object);
            controller.pageSize = 3;

            // Act
            PerfumeListViewModel result
                = (PerfumeListViewModel)controller.List(null, null, null, null, null, null, 2).Model;

            // Assert
            PagingInfo pageinfo = result.PagingInfo;
            Assert.AreEqual(pageinfo.CurrentPage, 2);
            Assert.AreEqual(pageinfo.ItemsPerPage, 3);
            Assert.AreEqual(pageinfo.TotalItems, 5);
            Assert.AreEqual(pageinfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Filter_Perfumes()
        {
            // Arrange - creating simualated repository
            Mock<IPerfumeRepository> mock = new Mock<IPerfumeRepository>();
            mock.Setup(m => m.Perfumes).Returns(new List<Perfume>
            {
                new Perfume { PerfumeId = 1, PerfumeName = "Fragrance1", Concentration="Conc1" },
                new Perfume { PerfumeId = 2, PerfumeName = "Fragrance2", Concentration="Conc2" },
                new Perfume { PerfumeId = 3, PerfumeName = "Fragrance3", Concentration="Conc3" },
                new Perfume { PerfumeId = 4, PerfumeName = "Fragrance4", Concentration="Conc2" },
                new Perfume { PerfumeId = 5, PerfumeName = "Fragrance5", Concentration="Conc5" }
            });

            // Arrange - creating controller
            PerfumeController controller = new PerfumeController(mock.Object);

            // Action
            List<Perfume> result = ((PerfumeListViewModel)controller.List(null, null, null, "Conc2", null, null, 1)
                .Model).Perfumes.ToList();

            // Assert
            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result[0].PerfumeName == "Fragrance2" && result[0].Concentration == "Conc2");
            Assert.IsTrue(result[1].PerfumeName == "Fragrance4" && result[1].Concentration == "Conc2");
        }

        [TestMethod]
        public void Can_Create_Categories()
        {
            // Arrange - creating simulated repository
            Mock<IPerfumeRepository>mock = new Mock<IPerfumeRepository>();
            mock.Setup(m => m.Perfumes).Returns(new List<Perfume>
            {
                new Perfume { PerfumeId = 1, PerfumeName = "Fragrance1", Concentration="Eau de parfum" },
                new Perfume { PerfumeId = 2, PerfumeName = "Fragrance2", Concentration="Eau de parfum" },
                new Perfume { PerfumeId = 3, PerfumeName = "Fragrance3", Concentration="Parfum" },
                new Perfume { PerfumeId = 4, PerfumeName = "Fragrance4", Concentration="Eau de toilette" }
            });

            // Arrange - creating controller
            NavController target = new NavController(mock.Object);

            // Action - creating categories set
            List<string> results = ((IEnumerable<string>)target.MenuConcentration().Model).ToList();

            // Assert
            Assert.AreEqual(results.Count, 3);
            Assert.AreEqual(results[0], "Eau de parfum");
            Assert.AreEqual(results[1], "Eau de toilette");
            Assert.AreEqual(results[2], "Parfum");
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            // Arrange - creating simulated repository
            Mock<IPerfumeRepository>mock = new Mock<IPerfumeRepository>();
            mock.Setup(m => m.Perfumes).Returns(new Perfume[]
            {
                new Perfume { PerfumeId = 1, PerfumeName = "Fragrance1", Concentration="Eau de parfum" },
                new Perfume { PerfumeId = 2, PerfumeName = "Fragrance2", Concentration="Eau de toilette" }
            });

            // Arrange - creating controller
            NavController target = new NavController(mock.Object);

            // Arrange - indicating selected category
            string categoryToSelect = "Eau de parfum";

            // Action
            string result = target.MenuConcentration(categoryToSelect).ViewBag.SelectedCategory;

            // Assert
            Assert.AreEqual(categoryToSelect, result);
        }

        [TestMethod]
        public void Generate_Category_Specific_Perfume_Count()
        {
            // Arrange - creating simulated repository
            Mock<IPerfumeRepository> mock = new Mock<IPerfumeRepository>();
            mock.Setup(m => m.Perfumes).Returns(new List<Perfume>
            {
                new Perfume { PerfumeId = 1, PerfumeName = "Fragrance1", Concentration="Conc1" },
                new Perfume { PerfumeId = 2, PerfumeName = "Fragrance2", Concentration="Conc2" },
                new Perfume { PerfumeId = 3, PerfumeName = "Fragrance3", Concentration="Conc1" },
                new Perfume { PerfumeId = 4, PerfumeName = "Fragrance4", Concentration="Conc2" },
                new Perfume { PerfumeId = 5, PerfumeName = "Fragrance5", Concentration="Conc3" }
            });

            // Arrange - creating controller
            PerfumeController controller = new PerfumeController(mock.Object);
            controller.pageSize = 3;

            // Action - perfume count testing for specific categories
            int res1 = ((PerfumeListViewModel)controller.List(null, null, null, "Conc1", null, null, 0).Model).PaggingInfo.TotalItems;
            int res2 = ((PerfumeListViewModel)controller.List(null, null, null, "Conc2", null, null, 0).Model).PaggingInfo.TotalItems;
            int res3 = ((PerfumeListViewModel)controller.List(null, null, null, "Conc3", null, null, 0).Model).PaggingInfo.TotalItems;
            int resAll = ((PerfumeListViewModel)controller.List(null, null, null, null, null, null, 0).Model).PaggingInfo.TotalItems;

            // Assert
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }
    }
}
