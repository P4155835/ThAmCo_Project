using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryManagementModel;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ThAmCo_WebServiceAPI.Dtos;
using ThAmCo_WebServiceAPI.Repository;

namespace ThAmCo_WebServiceAPI_Tests
{
    [TestClass]
    public class WebServiceAPI_UnitTests
    {

        // Testing selecting all.
        [TestMethod]
        public void TestGettingAllFromMock_Successful()
        {
            // Assign
            // collection to add to the mock database.
            var prods = new List<Product>
            {
                new Product { ThAmCo_Id = 1, Ean = "1", Name = "1", Description = "1", Price = 1, PriceForTen = 1, InStock = true, ExpectedRestock = null },
                new Product { ThAmCo_Id = 2, Ean = "2", Name = "2", Description = "2", Price = 2, PriceForTen = 2, InStock = true, ExpectedRestock = null },
                new Product { ThAmCo_Id = 3, Ean = "3", Name = "3", Description = "3", Price = 3, PriceForTen = 3, InStock = true, ExpectedRestock = null },
                new Product { ThAmCo_Id = 4, Ean = "4", Name = "4", Description = "4", Price = 4, PriceForTen = 4, InStock = true, ExpectedRestock = null },
                new Product { ThAmCo_Id = 5, Ean = "5", Name = "5", Description = "5", Price = 5, PriceForTen = 5, InStock = true, ExpectedRestock = null }
            }.AsQueryable();

            var mockProductDbSet = GetQueryableMockProductDbSet(prods);
            var mockContext = new Mock<ModelInventoryManagement>();
            mockContext.Setup(p => p.Products).Returns(mockProductDbSet.Object); // use mock list instead of db.Products.
            var repo = new ProductRepository(mockContext.Object);

            // Act
            var allProds = repo.SelectAll();

           // Assert
            Assert.AreEqual(5, allProds.Count());
        }

        // Testing selecting all from empty collection.
        [TestMethod]
        public void TestGettingAllFromMock_Successful_Empty()
        {
            // Assign
            // collection to add to the mock database.
            var prods = new List<Product>
            {

            }.AsQueryable();

            var mockProductDbSet = GetQueryableMockProductDbSet(prods);
            var mockContext = new Mock<ModelInventoryManagement>();
            mockContext.Setup(p => p.Products).Returns(mockProductDbSet.Object); // use mock list instead of db.Products.
            var repo = new ProductRepository(mockContext.Object);

            // Act
            var allProds = repo.SelectAll();

            // Assert
            Assert.AreEqual(0, allProds.Count());
        }

        // Testing getting by Id.
        [TestMethod]
        public void TestGettingByIdFromMock_Successful()
        {
            // Assign
            // collection to add to the mock database.
            var prods = new List<Product>
            {
                new Product { ThAmCo_Id = 1, Ean = "1", Name = "1", Description = "1", Price = 1, PriceForTen = 1, InStock = true, ExpectedRestock = null },
                new Product { ThAmCo_Id = 2, Ean = "2", Name = "2", Description = "2", Price = 2, PriceForTen = 2, InStock = true, ExpectedRestock = null },
                new Product { ThAmCo_Id = 3, Ean = "3", Name = "3", Description = "3", Price = 3, PriceForTen = 3, InStock = true, ExpectedRestock = null },
                new Product { ThAmCo_Id = 4, Ean = "4", Name = "4", Description = "4", Price = 4, PriceForTen = 4, InStock = true, ExpectedRestock = null },
                new Product { ThAmCo_Id = 5, Ean = "5", Name = "5", Description = "5", Price = 5, PriceForTen = 5, InStock = true, ExpectedRestock = null }
            }.AsQueryable();

            var mockProductDbSet = GetQueryableMockProductDbSet(prods);
            var mockContext = new Mock<ModelInventoryManagement>();
            mockContext.Setup(p => p.Products).Returns(mockProductDbSet.Object); // use mock list instead of db.Products.
            var repo = new ProductRepository(mockContext.Object);

            // Act
            var prod = repo.SelectById(1);

            // Assert
            Assert.AreEqual(1, prod.ThAmCo_Id);

        }

        // Testing getting by Id from empty collection.
        [TestMethod]
        public void TestGettingByIdFromMock_Successful_Empty()
        {
            // Assign
            // collection to add to the mock database.
            var prods = new List<Product>
            {

            }.AsQueryable();

            var mockProductDbSet = GetQueryableMockProductDbSet(prods);
            var mockContext = new Mock<ModelInventoryManagement>();
            mockContext.Setup(p => p.Products).Returns(mockProductDbSet.Object); // use mock list instead of db.Products.
            var repo = new ProductRepository(mockContext.Object);

            // Act
            var prod = repo.SelectById(1);

            // Assert
            Assert.IsNull(prod);

        }



        // helper function to create a mock Db Set.
        private static Mock<DbSet<Product>> GetQueryableMockProductDbSet(IQueryable<Product> prods)
        {
            var mockProductDbSet = new Mock<DbSet<Product>>();
            mockProductDbSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(prods.Provider);
            mockProductDbSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(prods.Expression);
            mockProductDbSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(prods.ElementType);
            mockProductDbSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(prods.GetEnumerator());
            mockProductDbSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => prods.FirstOrDefault(d => d.ThAmCo_Id == (int)ids[0]));

            return mockProductDbSet;
        }
    }
}
