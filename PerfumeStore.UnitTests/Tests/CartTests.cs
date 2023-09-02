using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using PerfumeStore.Domain.Entities;

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

    }
}
