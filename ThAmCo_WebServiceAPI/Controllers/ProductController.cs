using InventoryManagementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using ThAmCo_WebServiceAPI.Dtos;
using ThAmCo_WebServiceAPI.Repository;

namespace ThAmCo_WebServiceAPI.Controllers
{
    public class ProductController : ApiController
    {

        // dependency injection
        //https://youtu.be/ojOyv9h0mIk
        //https://www.youtube.com/watch?v=wm4ppHEOWmw&list=PLTgRMOcmRb3M9B7-XU6Fu9yc37cmB_sc_&index=6
        //https://www.asp.net/web-api/overview/advanced/dependency-injection

        // repository stuff
        //https://www.asp.net/web-api/overview/older-versions/creating-a-web-api-that-supports-crud-operations
        //https://www.asp.net/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
        //http://www.c-sharpcorner.com/uploadfile/736ca4/working-with-repository-pattern-in-webapi-2/
        //http://dotnetmentors.com/web-api/rest-based-crud-operations-with-asp-net-web-api.aspx
        //http://www.codeguru.com/csharp/.net/net_asp/mvc/using-the-repository-pattern-with-asp.net-mvc-and-entity-framework.htm

            //throw new HttpResponseException(HttpStatusCode.NotFound);



        private IProductRepository repository;
        //private IProductRepository repository = null;


        // constructor - default
        //public ProductController()
        //{
        //    this.repository = new ProductRepository();
        //}


        // constructor.
        // essentially means that the repository interface is not created in the controller, but rather passed through when constructed, through the bootstrap class for the DI.
        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }

        // All products or by ThAmCo_Id.
        // GET: api/Product
        public IEnumerable<ThAmCoProductDTO> GetProducts()
        {
            // try/catch needed?
            return repository.SelectAll();
        }

        // Product by ThAmCo_Id.
        // GET: api/Product/1
        public IHttpActionResult GetProducts(int id)
        {
            var prod = repository.SelectById(id);

            if (prod == null)
            {
                return NotFound(); // product wasn't found.
            }

            return Ok(prod); // good response, product found.

        }


        // // GETTING ALL PRODUCTS OR BY ID IN ONE FUNCTION
        //// All products or by ThAmCo_Id.
        //// GET: api/Product
        //public IEnumerable<ThAmCoProductDTO> GetProducts(int? id = null)
        //{
        //    if (id == null)
        //    {
        //        return repository.SelectAll(null);
        //    }
        //    else // if an integer was entered,
        //    {
        //        return repository.SelectAll(id);
        //       
        //
        //
        //       return (IEnumerable<ThAmCoProductDTO>)repository.SelectById(id); // issue with this - will seperate into two
        //    }                                                        // methods, as this one expects an IEnumerable. 
        //    // need expection handling?
        //}











        //// All products or by ThAmCo_Id.
        //// GET: api/Product
        //public IEnumerable<ThAmCoProductDTO> GetProducts(int? id = null)
        //{
        //    var prods = db.Products
        //             .AsEnumerable()
        //             .Select
        //             (p => new ThAmCoProductDTO
        //             {
        //                 ThAmCo_Id = p.ThAmCo_Id,
        //                 Ean = p.Ean,
        //                 Name = p.Name,
        //                 Description = p.Description,
        //                 Price = p.Price,
        //                 PriceForOne = p.PriceForOne,
        //                 PriceForTen = p.PriceForTen,
        //                 InStock = p.InStock,
        //                 ExpectedRestock = p.ExpectedRestock
        //             });

        //    if (id != null) // if an integer was entered,
        //    {
        //        prods = prods.Where(p => p.ThAmCo_Id == id);
        //    }

        //    return prods;
        //}





        //public IEnumerable<ThAmCoProductDTO> Get()    // get all products
        //{
        //    var all = db.Products; // gets all products from the database.

        //    var allProducts = all.Select(pr => new ThAmCoProductDTO()
        //        {
        //            SupplierProductId = pr.SupplierProductId,
        //            //SupplierName = pr.SupplierName,
        //            Ean = pr.Ean,
        //            //CategoryId = pr.CategoryId,
        //            //CategoryName = pr.CategoryName,
        //            //BrandId = pr.BrandId,
        //            //BrandName = pr.BrandName,
        //            Name = pr.Name,
        //            Description = pr.Description,
        //            Price = pr.Price,
        //            PriceForOne = pr.PriceForOne,
        //            PriceForTen = pr.PriceForTen,
        //            InStock = pr.InStock,
        //            //ExpectedRestock = pr.ExpectedRestock
        //        });

        //    return allProducts;    // return collection of dto's, retreived from the DB..
        //}

    }
}
