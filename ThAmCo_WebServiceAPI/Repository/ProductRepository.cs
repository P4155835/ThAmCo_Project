using InventoryManagementModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ThAmCo_WebServiceAPI.Dtos;

namespace ThAmCo_WebServiceAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        //private ModelInventoryManagement db = null;
        private ModelInventoryManagement db;

        // constructor.
        public ProductRepository(ModelInventoryManagement db)
        {
            this.db = db;
        }

        public IEnumerable<ThAmCoProductDTO> SelectAll()
        {
            return db.Products
                     .AsEnumerable()
                     .Select
                     (p => new ThAmCoProductDTO
                     {
                         ThAmCo_Id = p.ThAmCo_Id,
                         Ean = p.Ean,
                         Name = p.Name,
                         Description = p.Description,
                         Price = p.Price,
                         //PriceForOne = p.PriceForOne,
                         PriceForTen = p.PriceForTen,
                         InStock = p.InStock,
                         ExpectedRestock = p.ExpectedRestock
                     });
        }

        public ThAmCoProductDTO SelectById(int id)
        {
            try
            {
                //var prod = db.Products.Where(p => p.ThAmCo_Id == id).FirstOrDefault();
                var prod = db.Products.Find(id); // find product by ThAmCo_Id value in db context.

                // mapping to a dto.
                return (new ThAmCoProductDTO
                {
                    ThAmCo_Id = prod.ThAmCo_Id,
                    Ean = prod.Ean,
                    Name = prod.Name,
                    Description = prod.Description,
                    Price = prod.Price,
                    //PriceForOne = prod.PriceForOne,
                    PriceForTen = prod.PriceForTen,
                    InStock = prod.InStock,
                    ExpectedRestock = prod.ExpectedRestock
                });
            }
            
            catch // handling if value is not found, i.e api/Product/5 is non-existent.
            {
                Console.WriteLine("No Product of this value has been found.");
                return null;
            }

            // could have been done the way below, however not as efficient, as would need to grab all, then find the value.
            // using .Find and mapping means that the specific one is found and then created.
            //return SelectAll().Where(p => p.ThAmCo_Id == id).FirstOrDefault(); // should only ever get one, since ThAmCo_Id is a unique Id in DB.  
        }
    }
}