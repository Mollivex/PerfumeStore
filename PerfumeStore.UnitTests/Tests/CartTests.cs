using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using PerfumeStore.Domain.Entities;
using Moq;
using PerfumeStore.Domain.Abstract;
using PerfumeStore.WebUI.Controllers;
using System.Web.Mvc;
using PerfumeStore.WebUI.Models;
using System.Diagnostics;

namespace PerfumeStore.UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            // Arrange - creating a few test perfumes
            Perfume perfume1 = new Perfume { PerfumeId = 1, PerfumeName = "Fragrance1" };
            Perfume perfume2 = new Perfume { PerfumeId = 2, PerfumeName = "Fragrance2" };

            // Arrange - creating cart
            Cart cart = new Cart();

            // Action
            cart.AddItem(perfume1, 1);
            cart.AddItem(perfume2, 1);
            List<CartLine> results = cart.Lines.ToList();

            // Assert
            Assert.AreEqual(results.Count, 2);
            Assert.AreEqual(results[0].Perfume, perfume1);
            Assert.AreEqual(results[1].Perfume, perfume2);
        }

        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            // Arrange - creating a few test perfumes
            Perfume perfume1 = new Perfume { PerfumeId = 1, PerfumeName = "Fragrance1" };
            Perfume perfume2 = new Perfume { PerfumeId = 2, PerfumeName = "Fragrance2" };

            // Arrange - creating cart
            Cart cart = new Cart();

            // Action
            cart.AddItem(perfume1, 1);
            cart.AddItem(perfume2, 1);
            cart.AddItem(perfume1, 5);
            List<CartLine> results = cart.Lines.OrderBy(c => c.Perfume.PerfumeId).ToList();

            // Assert
            Assert.AreEqual(results.Count, 2);
            Assert.AreEqual(results[0].Quantity, 6);
            Assert.AreEqual(results[1].Quantity, 1);
        }

        [TestMethod]
        public void Can_Remove_Line()
        {
            // Arrange - creating a few test perfumes
            Perfume perfume1 = new Perfume { PerfumeId = 1, PerfumeName = "Fragrance1" };
            Perfume perfume2 = new Perfume { PerfumeId = 2, PerfumeName = "Fragrance2" };
            Perfume perfume3 = new Perfume { PerfumeId = 3, PerfumeName = "Fragrance3" };

            // Arrange - creating cart
            Cart cart = new Cart();

            // Arrange - adding a few perfumes to cart
            cart.AddItem(perfume1, 1);
            cart.AddItem(perfume2, 4);
            cart.AddItem(perfume3, 2);
            cart.AddItem(perfume2, 1);

            // Action
            cart.RemoveLines(perfume2);

            // Assert
            Assert.AreEqual(cart.Lines.Where(c => c.Perfume == perfume2).Count(), 0);
            Assert.AreEqual(cart.Lines.Count(), 2);
        }

        [TestMethod]
        public void Calculate_Cart_Total()
        {
            // Arrange - creating a few test perfumes
            Perfume perfume1 = new Perfume { PerfumeId = 1, PerfumeName = "Fragrance1", Price = 100 };
            Perfume perfume2 = new Perfume { PerfumeId = 2, PerfumeName = "Fragrance2", Price = 55 };

            // Arrange - creating cart
            Cart cart = new Cart();

            // Arrange - adding a few perfumes to cart
            cart.AddItem(perfume1, 1);
            cart.AddItem(perfume2, 1);
            cart.AddItem(perfume1, 5);
            decimal result = cart.ComputeTotalValue();

            // Assert
            Assert.AreEqual(result, 655);
        }

        [TestMethod]
        public void Can_Clear_Contents()
        {
            // Arrange - creating a few test perfumes
            Perfume perfume1 = new Perfume { PerfumeId = 1, PerfumeName = "Fragrance1", Price = 100 };
            Perfume perfume2 = new Perfume { PerfumeId = 2, PerfumeName = "Fragrance2", Price = 55 };

            // Arrange - creating cart
            Cart cart = new Cart();

            // Arrange - adding a few perfumes to cart
            cart.AddItem(perfume1, 1);
            cart.AddItem(perfume2, 1);
            cart.AddItem(perfume1, 5);
            cart.Clear();

            // Assert
            Assert.AreEqual(cart.Lines.Count(), 0);
        }

        /// <summary>
        /// Checking adding to cart
        /// </summary>
        [TestMethod]
        public void Can_Add_To_Cart()
        {
            // Arrange - creating simulated repository
            Mock<IPerfumeRepository> mock = new Mock<IPerfumeRepository>();
            mock.Setup(m => m.Perfumes).Returns(new List<Perfume>
            {
                new Perfume {PerfumeId = 1, PerfumeName = "Fragrance1", Category="Cat1"}
            }.AsQueryable());

            // Arrange - creating cart
            Cart cart = new Cart();

            // Arrange - creating controller
            CartController controller = new CartController(mock.Object, null);

            // Action - add perfume to the cart
            controller.AddToCart(cart, 1, null);

            // Assert
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToList()[0].Perfume.PerfumeId, 1);
        }

        /// <summary>
        /// After adding perfume to the cart, it goes to the cart screen
        /// </summary>
        [TestMethod]
        public void Adding_Perfume_To_Cart_Goes_To_Cart_Screen()
        {
            // Arrange - creating simulated repository
            Mock<IPerfumeRepository> mock = new Mock<IPerfumeRepository>();
            mock.Setup(m => m.Perfumes).Returns(new List<Perfume>
            {
                new Perfume {PerfumeId = 1, PerfumeName = "Fragrance1", Category="Cat1"}
            }.AsQueryable());

            // Arrange - creating cart
            Cart cart = new Cart();

            // Arrange - creating controller
            CartController controller = new CartController(mock.Object, null);

            // Action - adding perfume to cart
            RedirectToRouteResult result = controller.AddToCart(cart, 2, "myUrl");

            // Assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }


        /// <summary>
        /// Checking URL
        /// </summary>
        [TestMethod]
        public void Can_View_Cart_Contents()
        {
            // Arrange - creating cart
            Cart cart = new Cart();

            // Arrange - creating controller
            CartController target = new CartController(null,null);

            // Action - Index() action method call
            CartIndexViewModel result =
                (CartIndexViewModel)target.Index(cart, "myUrl").ViewData.Model;

            // Assert
            Assert.AreSame(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");
        }

        [TestMethod]
        public void Cannot_Chekout_Invalid_ShippingDetails()
        {
            // Arrange - creating simulated order processor
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            // Arrange - creating cart with item
            Cart cart = new Cart();
            cart.AddItem(new Perfume(), 1);

            // Arrange - creating controller
            CartController controller = new CartController(null, mock.Object);

            // Arrange - add error in model
            controller.ModelState.AddModelError("error", "error");

            // Action - trying to checkout
            ViewResult result = controller.Checkout(cart, new ShippingDetails());

            // Assert - checking that order isn't passed to the processor
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
                Times.Never());

            // Assert - checking that method return default view
            Assert.AreEqual("", result.ViewName);

            // Assert - checking that invalid model is passed to the view
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Can_Checkout_And_Submit_Order()
        {
            // Arrange - creating simulated order processor
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            // Arrange - creating cart with item
            Cart cart = new Cart();
            cart.AddItem(new Perfume(), 1);

            // Arrange - creating controller
            CartController controller = new CartController(null, mock.Object);

            // Action - trying to checkout
            ViewResult result = controller.Checkout(cart, new ShippingDetails());

            // Assert - checking that order is passed to the processor
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
                Times.Once());

            // Assert - checking that method return view
            Assert.AreEqual("Completed", result.ViewName);

            // Assert - checking that valid model is passed to the view
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);
        }
    }
}
