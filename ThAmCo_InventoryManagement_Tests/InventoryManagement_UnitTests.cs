using Microsoft.VisualStudio.TestTools.UnitTesting;
using InventoryManagementModel;
using ThAmCo_Project.Repository;
using ThAmCo_Project.Dtos;
using System.Collections.Generic;
using Moq;
using System.Data.Entity;
using System.Linq;

namespace ThAmCo_InventoryManagement_Tests
{
    [TestClass]
    public class InventoryManagement_UnitTests
    {
        // This is how the method is intended to be used.
        // registeredSuppliers containing a name used as both the SupplierName field for the data being added, also matching the supplierName paramater of the insert method call.
        [TestMethod]
        public void Insert_StandardSingleInsert()
        {
            // Arrange
            // collection of registered supplier names added to record, for when inserting (updating) to not remove those which are recognised from previous Insert.
            var registeredSuppliers = new List<string>();
            registeredSuppliers.Add("Test Supplier");

            // collection to add to the mock database.
            var ThAmCoProductDTOs = new List<ThAmCoProductDTO>
            {
                new ThAmCoProductDTO { SupplierProductId = 1, SupplierName = "Test Supplier", Ean = "1", CategoryId = 1, CategoryName = "1", BrandId = 1, BrandName = "1", Name = "Product 1", Description = "Product 1", Price = 1, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 2, SupplierName = "Test Supplier", Ean = "2", CategoryId = 2, CategoryName = "2", BrandId = 2, BrandName = "2", Name = "Product 2", Description = "Product 2", Price = 2, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 3, SupplierName = "Test Supplier", Ean = "3", CategoryId = 3, CategoryName = "3", BrandId = 3, BrandName = "3", Name = "Product 3", Description = "Product 3", Price = 3, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 4, SupplierName = "Test Supplier", Ean = "4", CategoryId = 4, CategoryName = "4", BrandId = 4, BrandName = "4", Name = "Product 4", Description = "Product 4", Price = 4, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 5, SupplierName = "Test Supplier", Ean = "5", CategoryId = 5, CategoryName = "5", BrandId = 5, BrandName = "5", Name = "Product 5", Description = "Product 5", Price = 5, PriceForTen = null, InStock = true, ExpectedRestock = null }
            };

            ModelInventoryManagement context = new ModelInventoryManagement();
            context.Products = GetQueryableMockProductDbSet().Object; // setting context.Products to be the mock Db set.
            var repo = new ImportProductRepository(context); // passing context through to the repo instance created.
            
            /* Doing the same sort of thing as this code (WebServiceAPI_UnitTests code), but just done in a slightly different way for purpose. Could change to be consistent at a later date.
            var mockProductDbSet = GetQueryableMockProductDbSet(prods);
            var mockContext = new Mock<ModelInventoryManagement>();
            mockContext.Setup(p => p.Products).Returns(mockProductDbSet.Object); // use mock list instead of db.Products.
            var repo = new ProductRepository(mockContext.Object);
            */


            // Act
            repo.Insert(ThAmCoProductDTOs, "Test Supplier", registeredSuppliers); // insert method from repo.

            // Assert
            Assert.AreEqual(5, context.Products.Count()); // 5 entries added to db set. check to be true.
        }

        // As the first test, using the method in the correct way, but adding two sets of data (from two different suppliers).
        // no removal is made as both times the necessary information is provided correctly (and from two different suppliers).
        [TestMethod]
        public void Insert_MultipleInsertsFromDifferentSuppliers()
        {
            // Arrange
            // collection of registered supplier names added to record, for when inserting (updating) to not remove those which are recognised from previous Insert.
            var registeredSuppliers = new List<string>();
            registeredSuppliers.Add("Test Supplier");
            registeredSuppliers.Add("Test Supplier 2");

            // collection to add to the mock database.
            var ThAmCoProductDTOs1 = new List<ThAmCoProductDTO>
            {
                new ThAmCoProductDTO { SupplierProductId = 1, SupplierName = "Test Supplier", Ean = "1", CategoryId = 1, CategoryName = "1", BrandId = 1, BrandName = "1", Name = "Product 1", Description = "Product 1", Price = 1, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 2, SupplierName = "Test Supplier", Ean = "2", CategoryId = 2, CategoryName = "2", BrandId = 2, BrandName = "2", Name = "Product 2", Description = "Product 2", Price = 2, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 3, SupplierName = "Test Supplier", Ean = "3", CategoryId = 3, CategoryName = "3", BrandId = 3, BrandName = "3", Name = "Product 3", Description = "Product 3", Price = 3, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 4, SupplierName = "Test Supplier", Ean = "4", CategoryId = 4, CategoryName = "4", BrandId = 4, BrandName = "4", Name = "Product 4", Description = "Product 4", Price = 4, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 5, SupplierName = "Test Supplier", Ean = "5", CategoryId = 5, CategoryName = "5", BrandId = 5, BrandName = "5", Name = "Product 5", Description = "Product 5", Price = 5, PriceForTen = null, InStock = true, ExpectedRestock = null }
            };
            // collection 2 to be added.
            var ThAmCoProductDTOs2 = new List<ThAmCoProductDTO>
            {
                new ThAmCoProductDTO { SupplierProductId = 1, SupplierName = "Test Supplier 2", Ean = "1", CategoryId = 1, CategoryName = "1", BrandId = 1, BrandName = "1", Name = "Product 1", Description = "Product 1", Price = 1, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 2, SupplierName = "Test Supplier 2", Ean = "2", CategoryId = 2, CategoryName = "2", BrandId = 2, BrandName = "2", Name = "Product 2", Description = "Product 2", Price = 2, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 3, SupplierName = "Test Supplier 2", Ean = "3", CategoryId = 3, CategoryName = "3", BrandId = 3, BrandName = "3", Name = "Product 3", Description = "Product 3", Price = 3, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 4, SupplierName = "Test Supplier 2", Ean = "4", CategoryId = 4, CategoryName = "4", BrandId = 4, BrandName = "4", Name = "Product 4", Description = "Product 4", Price = 4, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 5, SupplierName = "Test Supplier 2", Ean = "5", CategoryId = 5, CategoryName = "5", BrandId = 5, BrandName = "5", Name = "Product 5", Description = "Product 5", Price = 5, PriceForTen = null, InStock = true, ExpectedRestock = null }
            };

            ModelInventoryManagement context = new ModelInventoryManagement();
            context.Products = GetQueryableMockProductDbSet().Object; // setting context.Products to be the mock Db set.
            var repo = new ImportProductRepository(context); // passing context through to the repo instance created.


            // Act
            repo.Insert(ThAmCoProductDTOs1, "Test Supplier", registeredSuppliers); // insert method from repo.
            repo.Insert(ThAmCoProductDTOs2, "Test Supplier 2", registeredSuppliers); // insert method from repo.

            // Assert
            Assert.AreEqual(10, context.Products.Count()); // 5 entries added to db set. check to be true.
        }

        // Using insert in the correct way - calling the method twice, allowing method handling to update the necessary data being inputted which already existed.
        // If any data was inputted on the second time which was not there the first time, it would be added.
        // If any data was removed from the second set but was there on the first insert (when being called correctly with the right inputs and matching supplierName in array/call/collection), deletion of data which is no longer needed would also take place.
        [TestMethod]
        public void Insert_CheckForUpdatingOfDataFromAPreviousInsert()
        {
            // Arrange
            // collection of registered supplier names added to record, for when inserting (updating) to not remove those which are recognised from previous Insert.
            var registeredSuppliers = new List<string>();
            registeredSuppliers.Add("Test Supplier");

            // collection to add to the mock database.
            var ThAmCoProductDTOs = new List<ThAmCoProductDTO>
            {
                new ThAmCoProductDTO { SupplierProductId = 1, SupplierName = "Test Supplier", Ean = "1", CategoryId = 1, CategoryName = "1", BrandId = 1, BrandName = "1", Name = "Product 1", Description = "Product 1", Price = 1, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 2, SupplierName = "Test Supplier", Ean = "2", CategoryId = 2, CategoryName = "2", BrandId = 2, BrandName = "2", Name = "Product 2", Description = "Product 2", Price = 2, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 3, SupplierName = "Test Supplier", Ean = "3", CategoryId = 3, CategoryName = "3", BrandId = 3, BrandName = "3", Name = "Product 3", Description = "Product 3", Price = 3, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 4, SupplierName = "Test Supplier", Ean = "4", CategoryId = 4, CategoryName = "4", BrandId = 4, BrandName = "4", Name = "Product 4", Description = "Product 4", Price = 4, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 5, SupplierName = "Test Supplier", Ean = "5", CategoryId = 5, CategoryName = "5", BrandId = 5, BrandName = "5", Name = "Product 5", Description = "Product 5", Price = 5, PriceForTen = null, InStock = true, ExpectedRestock = null }
            };

            ModelInventoryManagement context = new ModelInventoryManagement();
            context.Products = GetQueryableMockProductDbSet().Object; // setting context.Products to be the mock Db set.
            var repo = new ImportProductRepository(context); // passing context through to the repo instance created.


            // Act
            repo.Insert(ThAmCoProductDTOs, "Test Supplier", registeredSuppliers); // insert method from repo.
            repo.Insert(ThAmCoProductDTOs, "Test Supplier", registeredSuppliers); // insert method from repo.

            // Assert
            Assert.AreEqual(5, context.Products.Count()); // 5 entries added to db set. check to be true.
        }

        // Using insert in the correct way again - calling the method twice, allowing method handling to update the necessary data being inputted which already existed.
        // Any data which was existent with the matching product SupplierName to the call's supplierName (which is also existent in the registeredProducts collection) on a previous insert, but is not existent in a new insert with the same supplierName, will be removed from the database, as is no longer available from supplier.
        [TestMethod]
        public void Insert_UpdatingWithDeletionOfObseleteData()
        {
            // Arrange
            // collection of registered supplier names added to record, for when inserting (updating) to not remove those which are recognised from previous Insert.
            var registeredSuppliers = new List<string>();
            registeredSuppliers.Add("Test Supplier");

            // collection to add to the mock database.
            var ThAmCoProductDTOs1 = new List<ThAmCoProductDTO>
            {
                new ThAmCoProductDTO { SupplierProductId = 1, SupplierName = "Test Supplier", Ean = "1", CategoryId = 1, CategoryName = "1", BrandId = 1, BrandName = "1", Name = "Product 1", Description = "Product 1", Price = 1, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 2, SupplierName = "Test Supplier", Ean = "2", CategoryId = 2, CategoryName = "2", BrandId = 2, BrandName = "2", Name = "Product 2", Description = "Product 2", Price = 2, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 3, SupplierName = "Test Supplier", Ean = "3", CategoryId = 3, CategoryName = "3", BrandId = 3, BrandName = "3", Name = "Product 3", Description = "Product 3", Price = 3, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 4, SupplierName = "Test Supplier", Ean = "4", CategoryId = 4, CategoryName = "4", BrandId = 4, BrandName = "4", Name = "Product 4", Description = "Product 4", Price = 4, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 5, SupplierName = "Test Supplier", Ean = "5", CategoryId = 5, CategoryName = "5", BrandId = 5, BrandName = "5", Name = "Product 5", Description = "Product 5", Price = 5, PriceForTen = null, InStock = true, ExpectedRestock = null }
            };

            // collection to add to the mock database.
            var ThAmCoProductDTOs2 = new List<ThAmCoProductDTO>
            {
                new ThAmCoProductDTO { SupplierProductId = 1, SupplierName = "Test Supplier", Ean = "1", CategoryId = 1, CategoryName = "1", BrandId = 1, BrandName = "1", Name = "Product 1", Description = "Product 1", Price = 1, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 2, SupplierName = "Test Supplier", Ean = "2", CategoryId = 2, CategoryName = "2", BrandId = 2, BrandName = "2", Name = "Product 2", Description = "Product 2", Price = 2, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 3, SupplierName = "Test Supplier", Ean = "3", CategoryId = 3, CategoryName = "3", BrandId = 3, BrandName = "3", Name = "Product 3", Description = "Product 3", Price = 3, PriceForTen = null, InStock = true, ExpectedRestock = null },
            };

            ModelInventoryManagement context = new ModelInventoryManagement();
            context.Products = GetQueryableMockProductDbSet().Object; // setting context.Products to be the mock Db set.
            var repo = new ImportProductRepository(context); // passing context through to the repo instance created.

            // Act
            repo.Insert(ThAmCoProductDTOs1, "Test Supplier", registeredSuppliers); // insert method from repo.
            repo.Insert(ThAmCoProductDTOs2, "Test Supplier", registeredSuppliers); // insert method from repo.

            // Assert
            Assert.AreEqual(3, context.Products.Count()); // 5 entries added to db set. check to be true.
        }

        // Using insert in the correct way again - calling the method twice, allowing method handling to update the necessary data being inputted which already existed.
        // If any data was inputted on the second time call with the same name which was not there the first time, it would be added.
        [TestMethod]
        public void Insert_UpdatingWithMoreDataThanPreviousInsert()
        {
            // Arrange
            // collection of registered supplier names added to record, for when inserting (updating) to not remove those which are recognised from previous Insert.
            var registeredSuppliers = new List<string>();
            registeredSuppliers.Add("Test Supplier");

            // collection to add to the mock database.
            var ThAmCoProductDTOs1 = new List<ThAmCoProductDTO>
            {
                new ThAmCoProductDTO { SupplierProductId = 1, SupplierName = "Test Supplier", Ean = "1", CategoryId = 1, CategoryName = "1", BrandId = 1, BrandName = "1", Name = "Product 1", Description = "Product 1", Price = 1, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 2, SupplierName = "Test Supplier", Ean = "2", CategoryId = 2, CategoryName = "2", BrandId = 2, BrandName = "2", Name = "Product 2", Description = "Product 2", Price = 2, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 3, SupplierName = "Test Supplier", Ean = "3", CategoryId = 3, CategoryName = "3", BrandId = 3, BrandName = "3", Name = "Product 3", Description = "Product 3", Price = 3, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 4, SupplierName = "Test Supplier", Ean = "4", CategoryId = 4, CategoryName = "4", BrandId = 4, BrandName = "4", Name = "Product 4", Description = "Product 4", Price = 4, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 5, SupplierName = "Test Supplier", Ean = "5", CategoryId = 5, CategoryName = "5", BrandId = 5, BrandName = "5", Name = "Product 5", Description = "Product 5", Price = 5, PriceForTen = null, InStock = true, ExpectedRestock = null }
            };

            // collection to add to the mock database.
            var ThAmCoProductDTOs2 = new List<ThAmCoProductDTO>
            {
                new ThAmCoProductDTO { SupplierProductId = 1, SupplierName = "Test Supplier", Ean = "1", CategoryId = 1, CategoryName = "1", BrandId = 1, BrandName = "1", Name = "Product 1", Description = "Product 1", Price = 1, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 2, SupplierName = "Test Supplier", Ean = "2", CategoryId = 2, CategoryName = "2", BrandId = 2, BrandName = "2", Name = "Product 2", Description = "Product 2", Price = 2, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 3, SupplierName = "Test Supplier", Ean = "3", CategoryId = 3, CategoryName = "3", BrandId = 3, BrandName = "3", Name = "Product 3", Description = "Product 3", Price = 3, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 4, SupplierName = "Test Supplier", Ean = "4", CategoryId = 4, CategoryName = "4", BrandId = 4, BrandName = "4", Name = "Product 4", Description = "Product 4", Price = 4, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 5, SupplierName = "Test Supplier", Ean = "5", CategoryId = 5, CategoryName = "5", BrandId = 5, BrandName = "5", Name = "Product 5", Description = "Product 5", Price = 5, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 6, SupplierName = "Test Supplier", Ean = "6", CategoryId = 3, CategoryName = "6", BrandId = 6, BrandName = "6", Name = "Product 6", Description = "Product 6", Price = 6, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 7, SupplierName = "Test Supplier", Ean = "7", CategoryId = 4, CategoryName = "7", BrandId = 7, BrandName = "7", Name = "Product 7", Description = "Product 7", Price = 7, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 8, SupplierName = "Test Supplier", Ean = "8", CategoryId = 5, CategoryName = "8", BrandId = 8, BrandName = "8", Name = "Product 8", Description = "Product 8", Price = 8, PriceForTen = null, InStock = true, ExpectedRestock = null }
            };

            ModelInventoryManagement context = new ModelInventoryManagement();
            context.Products = GetQueryableMockProductDbSet().Object; // setting context.Products to be the mock Db set.
            var repo = new ImportProductRepository(context); // passing context through to the repo instance created.

            // Act
            repo.Insert(ThAmCoProductDTOs1, "Test Supplier", registeredSuppliers); // insert method from repo.
            repo.Insert(ThAmCoProductDTOs2, "Test Supplier", registeredSuppliers); // insert method from repo.

            // Assert
            Assert.AreEqual(8, context.Products.Count()); // 5 entries added to db set. check to be true.
        }

        // Same collection being added by two different suppliers - only 5 are kept, because on second run, update is occuring on each as they are the same values.
        // this occurs regardless of the SupplierName of the data not matching a registeredSuppliers value.
        // Note that once an invalid insert attempt occured (supplierName not matching a registeredSuppliers value), the data will be cleansed as the entries "SupplierName" does not match a registeredSuppliers value.
        [TestMethod]
        public void Insert_UpdatingWithNonMatchingSupplierNameOfData()
        {
            // Arrange
            // collection of registered supplier names added to record, for when inserting (updating) to not remove those which are recognised from previous Insert.
            var registeredSuppliers = new List<string>();
            registeredSuppliers.Add("Test Supplier");
            registeredSuppliers.Add("Test Supplier 2");

            // collection to add to the mock database.
            var ThAmCoProductDTOs = new List<ThAmCoProductDTO>
            {
                new ThAmCoProductDTO { SupplierProductId = 1, SupplierName = "Supplier 1", Ean = "1", CategoryId = 1, CategoryName = "1", BrandId = 1, BrandName = "1", Name = "Product 1", Description = "Product 1", Price = 1, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 2, SupplierName = "Supplier 2", Ean = "2", CategoryId = 2, CategoryName = "2", BrandId = 2, BrandName = "2", Name = "Product 2", Description = "Product 2", Price = 2, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 3, SupplierName = "Supplier 3", Ean = "3", CategoryId = 3, CategoryName = "3", BrandId = 3, BrandName = "3", Name = "Product 3", Description = "Product 3", Price = 3, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 4, SupplierName = "Supplier 4", Ean = "4", CategoryId = 4, CategoryName = "4", BrandId = 4, BrandName = "4", Name = "Product 4", Description = "Product 4", Price = 4, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 5, SupplierName = "Supplier 5", Ean = "5", CategoryId = 5, CategoryName = "5", BrandId = 5, BrandName = "5", Name = "Product 5", Description = "Product 5", Price = 5, PriceForTen = null, InStock = true, ExpectedRestock = null }
            };

            // Act
            ModelInventoryManagement context = new ModelInventoryManagement();
            context.Products = GetQueryableMockProductDbSet().Object; // setting context.Products to be the mock Db set.

            var repo = new ImportProductRepository(context); // passing context through to the repo instance created.
            repo.Insert(ThAmCoProductDTOs, "Test Supplier", registeredSuppliers); // insert method from repo.
            repo.Insert(ThAmCoProductDTOs, "Test Supplier 2", registeredSuppliers); // insert method from repo.


            // Assert
            Assert.AreEqual(5, context.Products.Count()); // 5 entries added to db set. check to be true.
        }

        // Passing through a null as the collection - expected to add nothing.
        [TestMethod]
        public void Insert_SuccessfulHandling_NullCollectionUsed()
        {
            // Arrange
            // collection of registered supplier names added to record, for when inserting (updating) to not remove those which are recognised from previous Insert.
            var registeredSuppliers = new List<string>();
            registeredSuppliers.Add("Test Supplier");

            ModelInventoryManagement context = new ModelInventoryManagement();
            context.Products = GetQueryableMockProductDbSet().Object; // setting context.Products to be the mock Db set.
            var repo = new ImportProductRepository(context); // passing context through to the repo instance created.

            // Act
            repo.Insert(null, "Test Supplier", registeredSuppliers); // insert method from repo.

            // Assert
            Assert.AreEqual(0, context.Products.Count()); // 5 entries added to db set. check to be true.
        }

        // Non-registered supplier attempting to add to collection. Limited to only those registered in the collection passed through.
        // the collection is important in the management of cleaning outdated information from the database.
        [TestMethod]
        public void Insert_NonRegisteredSupplier()
        {
            // Arrange
            // collection of registered supplier names added to record, for when inserting (updating) to not remove those which are recognised from previous Insert.
            var registeredSuppliers = new List<string>();


            // collection to add to the mock database.
            var ThAmCoProductDTOs = new List<ThAmCoProductDTO>
            {
                new ThAmCoProductDTO { SupplierProductId = 1, SupplierName = "Test Supplier", Ean = "1", CategoryId = 1, CategoryName = "1", BrandId = 1, BrandName = "1", Name = "Product 1", Description = "Product 1", Price = 1, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 2, SupplierName = "Test Supplier", Ean = "2", CategoryId = 2, CategoryName = "2", BrandId = 2, BrandName = "2", Name = "Product 2", Description = "Product 2", Price = 2, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 3, SupplierName = "Test Supplier", Ean = "3", CategoryId = 3, CategoryName = "3", BrandId = 3, BrandName = "3", Name = "Product 3", Description = "Product 3", Price = 3, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 4, SupplierName = "Test Supplier", Ean = "4", CategoryId = 4, CategoryName = "4", BrandId = 4, BrandName = "4", Name = "Product 4", Description = "Product 4", Price = 4, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 5, SupplierName = "Test Supplier", Ean = "5", CategoryId = 5, CategoryName = "5", BrandId = 5, BrandName = "5", Name = "Product 5", Description = "Product 5", Price = 5, PriceForTen = null, InStock = true, ExpectedRestock = null }
            };

            ModelInventoryManagement context = new ModelInventoryManagement();
            context.Products = GetQueryableMockProductDbSet().Object; // setting context.Products to be the mock Db set.
            var repo = new ImportProductRepository(context); // passing context through to the repo instance created.


            // Act
            repo.Insert(ThAmCoProductDTOs, "Test Supplier", registeredSuppliers); // insert method from repo.

            // Assert
            Assert.AreEqual(0, context.Products.Count()); // 5 entries added to db set. check to be true.
        }

        // Non-registered supplier attempting to add to collection. Limited to only those registered in the collection passed through.
        // the collection is important in the management of cleaning outdated information from the database.
        [TestMethod]
        public void Insert_RegisteredSupplierFollowedByAnUnregisteredSupplierTryingToInsertSameData()
        {
            // Arrange
            // collection of registered supplier names added to record, for when inserting (updating) to not remove those which are recognised from previous Insert.
            // Using a recognised supplier to add a set, then an unrecognised supplier trying to add (incidentally the same set, but will apply for any set) to the context.
            var registeredSuppliers = new List<string>();
            registeredSuppliers.Add("Test Supplier");

            // collection to add to the mock database.
            var ThAmCoProductDTOs = new List<ThAmCoProductDTO>
            {
                new ThAmCoProductDTO { SupplierProductId = 1, SupplierName = "Test Supplier", Ean = "1", CategoryId = 1, CategoryName = "1", BrandId = 1, BrandName = "1", Name = "Product 1", Description = "Product 1", Price = 1, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 2, SupplierName = "Test Supplier", Ean = "2", CategoryId = 2, CategoryName = "2", BrandId = 2, BrandName = "2", Name = "Product 2", Description = "Product 2", Price = 2, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 3, SupplierName = "Test Supplier", Ean = "3", CategoryId = 3, CategoryName = "3", BrandId = 3, BrandName = "3", Name = "Product 3", Description = "Product 3", Price = 3, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 4, SupplierName = "Test Supplier", Ean = "4", CategoryId = 4, CategoryName = "4", BrandId = 4, BrandName = "4", Name = "Product 4", Description = "Product 4", Price = 4, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 5, SupplierName = "Test Supplier", Ean = "5", CategoryId = 5, CategoryName = "5", BrandId = 5, BrandName = "5", Name = "Product 5", Description = "Product 5", Price = 5, PriceForTen = null, InStock = true, ExpectedRestock = null }
            };

            ModelInventoryManagement context = new ModelInventoryManagement();
            context.Products = GetQueryableMockProductDbSet().Object; // setting context.Products to be the mock Db set.
            var repo = new ImportProductRepository(context); // passing context through to the repo instance created.


            // Act
            repo.Insert(ThAmCoProductDTOs, "Test Supplier", registeredSuppliers); // insert method from repo.
            repo.Insert(ThAmCoProductDTOs, "Non Registered Supplier", registeredSuppliers); // insert method from repo.

            // Assert
            Assert.AreEqual(5, context.Products.Count()); // 5 entries added to db set. check to be true.
        }
        
        // Non-registered supplier attempting to add to collection. Limited to only those registered in the collection passed through.
        // the collection is important in the management of cleaning outdated information from the database.
        [TestMethod]
        public void Insert_RegisteredSupplierFollowedBySameSupplierNowUnRegisteredAndAttemptingToInsert()
        {
            // Arrange
            // collection of registered supplier names added to record, for when inserting (updating) to not remove those which are recognised from previous Insert.
            var registeredSuppliers = new List<string>();
            registeredSuppliers.Add("Test Supplier");

            // collection to add to the mock database.
            var ThAmCoProductDTOs = new List<ThAmCoProductDTO>
            {
                new ThAmCoProductDTO { SupplierProductId = 1, SupplierName = "Test Supplier", Ean = "1", CategoryId = 1, CategoryName = "1", BrandId = 1, BrandName = "1", Name = "Product 1", Description = "Product 1", Price = 1, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 2, SupplierName = "Test Supplier", Ean = "2", CategoryId = 2, CategoryName = "2", BrandId = 2, BrandName = "2", Name = "Product 2", Description = "Product 2", Price = 2, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 3, SupplierName = "Test Supplier", Ean = "3", CategoryId = 3, CategoryName = "3", BrandId = 3, BrandName = "3", Name = "Product 3", Description = "Product 3", Price = 3, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 4, SupplierName = "Test Supplier", Ean = "4", CategoryId = 4, CategoryName = "4", BrandId = 4, BrandName = "4", Name = "Product 4", Description = "Product 4", Price = 4, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 5, SupplierName = "Test Supplier", Ean = "5", CategoryId = 5, CategoryName = "5", BrandId = 5, BrandName = "5", Name = "Product 5", Description = "Product 5", Price = 5, PriceForTen = null, InStock = true, ExpectedRestock = null }
            };

            ModelInventoryManagement context = new ModelInventoryManagement();
            context.Products = GetQueryableMockProductDbSet().Object; // setting context.Products to be the mock Db set.
            var repo = new ImportProductRepository(context); // passing context through to the repo instance created.


            // Act
            repo.Insert(ThAmCoProductDTOs, "Test Supplier", registeredSuppliers); // insert method from repo.
            registeredSuppliers.Remove("Test Supplier");
            repo.Insert(ThAmCoProductDTOs, "Test Supplier", registeredSuppliers); // insert method from repo.

            // Assert
            Assert.AreEqual(0, context.Products.Count()); // 5 entries added to db set. check to be true.
        }

        // String passed through matching a registeredSuppliers value BUT data's SupplierName fields not matching same as supplierName passed through in insert.
        // This works - with the string passed through matching that of a registered supplier..
        // HOWEVER, the data's SupplierName does not match that of a registeredSupplier.
        // This means that when cleaning up the collection when a next insert does not have a string which matches a registeredSupplier, this data will be deleted.
        [TestMethod]
        public void Insert_LimitationWithSupplierNameFieldOfDataNotMatchingARegisteredSupplierNoInvalidCall()
        {
            // Arrange
                // collection of registered supplier names added to record, for when inserting (updating) to not remove those which are recognised from previous Insert.
            var registeredSuppliers = new List<string>();
            registeredSuppliers.Add("Test Supplier");

                // collection to add to the mock database.
            var ThAmCoProductDTOs = new List<ThAmCoProductDTO>
            {
                new ThAmCoProductDTO { SupplierProductId = 1, SupplierName = "Supplier 1", Ean = "1", CategoryId = 1, CategoryName = "1", BrandId = 1, BrandName = "1", Name = "Product 1", Description = "Product 1", Price = 1, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 2, SupplierName = "Supplier 2", Ean = "2", CategoryId = 2, CategoryName = "2", BrandId = 2, BrandName = "2", Name = "Product 2", Description = "Product 2", Price = 2, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 3, SupplierName = "Supplier 3", Ean = "3", CategoryId = 3, CategoryName = "3", BrandId = 3, BrandName = "3", Name = "Product 3", Description = "Product 3", Price = 3, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 4, SupplierName = "Supplier 4", Ean = "4", CategoryId = 4, CategoryName = "4", BrandId = 4, BrandName = "4", Name = "Product 4", Description = "Product 4", Price = 4, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 5, SupplierName = "Supplier 5", Ean = "5", CategoryId = 5, CategoryName = "5", BrandId = 5, BrandName = "5", Name = "Product 5", Description = "Product 5", Price = 5, PriceForTen = null, InStock = true, ExpectedRestock = null }
            };

            // Act
            ModelInventoryManagement context = new ModelInventoryManagement();
            context.Products = GetQueryableMockProductDbSet().Object; // setting context.Products to be the mock Db set.

            var repo = new ImportProductRepository(context); // passing context through to the repo instance created.
            repo.Insert(ThAmCoProductDTOs, "Test Supplier", registeredSuppliers); // insert method from repo.
            repo.Insert(ThAmCoProductDTOs, "Test Supplier", registeredSuppliers); // insert method from repo.

            // Assert
            Assert.AreEqual(5, context.Products.Count()); // 5 entries added to db set. check to be true.
        }

        // This test highlights the issue of an invalid call (string of supplierName in call not being a valid supplier).
        // As can be seen, when it occurs that data has been entered from suppliers, where the SupplierName of the data has not matched the supplierName passed through,
        // and an invalid call (as described) occurs, the data which is not associated in the database with a valid supplier is removed (can be seen in this test).
        [TestMethod]
        public void Insert_LimitationWithSupplierNameFieldOfDataNotMatchingARegisteredSupplierWithInvalidCall()
        {
            // Arrange
            // collection of registered supplier names added to record, for when inserting (updating) to not remove those which are recognised from previous Insert.
            var registeredSuppliers = new List<string>();
            registeredSuppliers.Add("Test Supplier");

            // collection to add to the mock database.
            var ThAmCoProductDTOs = new List<ThAmCoProductDTO>
            {
                new ThAmCoProductDTO { SupplierProductId = 1, SupplierName = "Supplier 1", Ean = "1", CategoryId = 1, CategoryName = "1", BrandId = 1, BrandName = "1", Name = "Product 1", Description = "Product 1", Price = 1, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 2, SupplierName = "Supplier 2", Ean = "2", CategoryId = 2, CategoryName = "2", BrandId = 2, BrandName = "2", Name = "Product 2", Description = "Product 2", Price = 2, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 3, SupplierName = "Supplier 3", Ean = "3", CategoryId = 3, CategoryName = "3", BrandId = 3, BrandName = "3", Name = "Product 3", Description = "Product 3", Price = 3, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 4, SupplierName = "Supplier 4", Ean = "4", CategoryId = 4, CategoryName = "4", BrandId = 4, BrandName = "4", Name = "Product 4", Description = "Product 4", Price = 4, PriceForTen = null, InStock = true, ExpectedRestock = null },
                new ThAmCoProductDTO { SupplierProductId = 5, SupplierName = "Supplier 5", Ean = "5", CategoryId = 5, CategoryName = "5", BrandId = 5, BrandName = "5", Name = "Product 5", Description = "Product 5", Price = 5, PriceForTen = null, InStock = true, ExpectedRestock = null }
            };

            // Act
            ModelInventoryManagement context = new ModelInventoryManagement();
            context.Products = GetQueryableMockProductDbSet().Object; // setting context.Products to be the mock Db set.

            var repo = new ImportProductRepository(context); // passing context through to the repo instance created.
            repo.Insert(ThAmCoProductDTOs, "Test Supplier", registeredSuppliers); // insert method from repo.
            repo.Insert(ThAmCoProductDTOs, "Test Supplier", registeredSuppliers); // insert method from repo.
            repo.Insert(ThAmCoProductDTOs, "test", registeredSuppliers); // insert method from repo.


            // Assert
            Assert.AreEqual(0, context.Products.Count()); // 5 entries added to db set. check to be true.
        }


        // helper function to create a mock Db Set.
        private static Mock<DbSet<Product>> GetQueryableMockProductDbSet()
        {
            var data = new List<Product>();

            var mockProductDbSet = new Mock<DbSet<Product>>();
            mockProductDbSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.AsQueryable().Provider);
            mockProductDbSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.AsQueryable().Expression);
            mockProductDbSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.AsQueryable().ElementType);
            mockProductDbSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockProductDbSet.Setup(m => m.Add(It.IsAny<Product>())).Callback<Product>(data.Add);
            mockProductDbSet.Setup(m => m.Remove(It.IsAny<Product>())).Callback<Product>(p => data.Remove(p));
            mockProductDbSet.Setup(m => m.RemoveRange(It.IsAny<IEnumerable<Product>>())).Callback<IEnumerable<Product>>(ts =>
            {
                foreach (var t in ts.ToList()) { data.Remove(t); }
            });

            return mockProductDbSet;
        }
    }
}
