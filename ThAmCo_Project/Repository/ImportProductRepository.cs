using InventoryManagementModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using ThAmCo_Project.Dtos;

namespace ThAmCo_Project.Repository
{
    public class ImportProductRepository : IImportProductRepository
    {
        private ModelInventoryManagement db;

        // contructor.
        public ImportProductRepository(ModelInventoryManagement db)
        {
            this.db = db;
        }

        public void Insert(IEnumerable<ThAmCoProductDTO> prodCollection, string supplierName, List<string> registeredSuppliers)
        {
            // if passed through value is not null (i.e didn't time out and return null, and has gathered the data),
            // And the passed through string matches the supplierName passed through

            if ((prodCollection != null) && (registeredSuppliers.Contains(supplierName)))
            {
                // mapping passed through collection of DTO's to collection of products.
                var mappedProducts = prodCollection
                .AsEnumerable()
                .Select
                     (p => new Product                                  // mapping each ThAmCoProductDTO to a Product to add to db.
                     {
                         // ThAmCo_Id = should be set automatically in db.
                         SupplierProductId = p.SupplierProductId,
                         SupplierName = p.SupplierName,
                         Ean = p.Ean,
                         CategoryId = p.CategoryId,
                         CategoryName = p.CategoryName,
                         BrandId = p.BrandId,
                         BrandName = p.BrandName,
                         Name = p.Name,
                         Description = p.Description,
                         Price = p.Price,
                         //PriceForOne = p.PriceForOne,
                         PriceForTen = p.PriceForTen,
                         InStock = p.InStock,
                         ExpectedRestock = p.ExpectedRestock,
                     });

               
                // adding/updating to context as applicable - checking if passed through prods exist. If existent, updating, if not, add.
                foreach (var prod in mappedProducts)
                {
                    var thisProd = db.Products.Where(p => p.SupplierProductId == prod.SupplierProductId && p.SupplierName == prod.SupplierName).FirstOrDefault();

                    if(thisProd != null)
                    {
                        //thisProd = prod;
                        Update(thisProd, prod); // maps prod to thisProd in db. Done so to avoid also updating the ThAmCo_Id each time update is ran.
                    }
                    else
                    {
                        db.Products.Add(prod);
                    }  
                }

              
                // deletion handling of previous data in db where applicable.
                List<Product> alteredMappedProducts = mappedProducts.ToList(); // made to list so that collection can be removed.

                try
                {
                    var supplierDbProds = db.Products.Where(p => p.SupplierName == supplierName).ToList(); // grabbing relevant entries depending on if the string passed through is the same.

                    foreach (var prod in supplierDbProds)
                    {
                        var thisProd = alteredMappedProducts.Where(p => p.SupplierProductId == prod.SupplierProductId).FirstOrDefault();

                        if (thisProd == null)
                        {
                            db.Products.Remove(prod);
                        }
                        else
                        {
                            alteredMappedProducts.Remove(thisProd);
                        }

                    }  
                }
                catch
                {
                    Console.WriteLine("Argument Null Exception Issue.");
                    Console.WriteLine("This is due to supplierDbProds attempting to be assigned a collection with associated supplierName (passed through to Insert function), which was not valid.");
                }
                
            }
            else if (prodCollection == null) // If service collection is still in database even though on this cache attempt no collection was gathered, delete as outdated.
            {
                try
                { 
                    // removing entries from this supplier - supplier isn't available.
                    var deleteCol = db.Products.Where(p => p.SupplierName == supplierName);
                    db.Products.RemoveRange(deleteCol);
                }
                catch
                {
                    Console.WriteLine("No entries in databased matched that of the service supplierName passed through.");
                }
            }
            else // supplierName was not found in registeredSuppliers - cannot add, and database is cleaned of any data (using their SupplierName) at all which does not match the registeredSuppliers.
            {
                try
                {
                    var deleteProd = db.Products.Where(p => !registeredSuppliers.Contains(p.SupplierName));
                    // var deleteProd = db.Products.Where(p => supplierName != p.SupplierName); // wouldn't work, as deletion of other services data would be deleted. need to compare SupplierName variable of each entry in db to the collection of registered supplier names in the collection, As seen above.
                    db.Products.RemoveRange(deleteProd);
                }
                catch
                {
                    Console.WriteLine("Unregistered supplier attempting to insert to database.");
                    Console.WriteLine("Ensure supplier is registered.");
                    Console.WriteLine("No collections were found in the database from this supplier. If they were, they would be removed as are now outdated and unnecessary for inventory.");
                    Console.WriteLine("The passed through value for supplierName was not found in the registered suppliers");
                }
            }
            Save(); // Save all changes.
        }

        public void Update(Product thisProd, Product prod)
        {
            thisProd.SupplierName = prod.SupplierName;
            thisProd.SupplierProductId = prod.SupplierProductId;
            thisProd.Ean = prod.Ean;
            thisProd.CategoryId = prod.CategoryId;
            thisProd.CategoryName = prod.CategoryName;
            thisProd.BrandId = prod.BrandId;
            thisProd.BrandName = prod.BrandName;
            thisProd.Name = prod.Name;
            thisProd.Description = prod.Description;
            thisProd.Price = prod.Price;
            thisProd.PriceForTen = prod.PriceForTen;
            thisProd.InStock = prod.InStock;
            thisProd.ExpectedRestock = prod.ExpectedRestock;
        }

        public void Save()
        {
            try
            {
                lock (db)
                {
                    db.SaveChanges();
                    //int x = db.SaveChangesAsync().Result;
                }
            }
            catch (DbEntityValidationException ex) // handling exception to detail the issue. Not my own code.
            {
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    // Get entry

                    DbEntityEntry entry = item.Entry;
                    string entityTypeName = entry.Entity.GetType().Name;

                    // Display or log error messages

                    foreach (DbValidationError subItem in item.ValidationErrors)
                    {
                        string message = string.Format("Error '{0}' occurred in {1} at {2}",
                                 subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                        Console.WriteLine(message);
                    }
                }
            }
        }
    }
}
