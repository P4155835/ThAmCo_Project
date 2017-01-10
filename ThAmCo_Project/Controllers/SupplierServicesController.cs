using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ThAmCo_Project.Dtos;
using ThAmCo_Project.Repository;
using ThAmCo_Project.ViewModels;

namespace ThAmCo_Project.Controllers
{
    public class SupplierServicesController : ApiController
    {
        private IImportProductRepository repository;

        // constructor - essentially allows the repository interface to not be created in the controller, but rather passed through when constructed, through the bootstrap class for the DI.
        public SupplierServicesController(IImportProductRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Products
        [HttpGet]
        [Route("api/Product")]
        public async Task<IEnumerable<ThAmCoProductDTO>> GetProductsAsnyc()
        {
            ProductCompilerViewModel Pc = new ProductCompilerViewModel(); // creating viewModel, where grabbing is handled.
            // Gathering from suppliers.
            Task<IEnumerable<ThAmCoProductDTO>> UndercuttersProds = Pc.getUndercuttersProductsAsync(); // getting all products, assigning to this value.
            Task<IEnumerable<ThAmCoProductDTO>> DodgyDealersProds = Pc.getDodgyDealersProductsAsync(); // getting all products, assigning to this value.
            Task<IEnumerable<ThAmCoProductDTO>> BazzasBazaarProds = Pc.getBazzasBazaarProductsAsync(); // getting all products, assigning to this value.

            // Future implementation: - This collection could eventually be moved to a database, and handled through that, rather than statically in this method.
            // updated when new service is implemented - if new collection is not done, possibility that collection of data being removed on next cache.
            List<string> registeredSuppliers = new List<string>();
            registeredSuppliers.Add("Undercutters");
            registeredSuppliers.Add("Dodgy Dealers");
            registeredSuppliers.Add("Bazzas Bazaar");
            
            // calling insert on each.
            repository.Insert(await UndercuttersProds, "Undercutters", registeredSuppliers);
            repository.Insert(await DodgyDealersProds, "Dodgy Dealers", registeredSuppliers);
            repository.Insert(await BazzasBazaarProds, "Bazzas Bazaar", registeredSuppliers);

            return null;
        }
















        //    HebbraCoModel.ModelHebbraCo db = new HebbraCoModel.ModelHebbraCo();     // creating a new instance of the database, getting a reference to refer to.

        //    public ActionResult Index()
        //    {
        //        var bu = db.BusinessUnits.Where(b => b.Active == true);  // create a variable holding all entries of the BusinessUnit section. Only shows results which are "Active" (true rather than false).

        //        return View(bu);            // return the view along with the variable created above being passed through.
        //    }

        //    // GET: BusinessUnit/Details/5
        //    public ActionResult Details(int id = 0) // details option.
        //    {
        //        BusinessUnit bu = db.BusinessUnits.Find(id);    // create a variable of type BusinessUnit and set it to the value of id found within the database.

        //        if (bu == null)                                 // if the value is null (no BusinessUnit found).
        //        {
        //            return HttpNotFound();                      // return a HttpNotFound error.
        //        }
        //        return View(bu);                                // if there is no problems with the creation of a new BusinessUnit and one has been found in database with the required value, return the view with this BusinessUnit.
        //    }

        //    // GET: BusinessUnit/Create
        //    public ActionResult Create()
        //    {
        //        return View();
        //    }

        //    // POST: BusinessUnit/Create
        //    [HttpPost]
        //    public ActionResult Create(BusinessUnit businessUnit)
        //    {

        //        if (ModelState.IsValid)                  // if the modelState is valid,
        //        {
        //            businessUnit.Active = true;         // set active to true.
        //            db.BusinessUnits.Add(businessUnit); // add businessUnit to BusinessUnits in database.
        //            db.SaveChanges();                   // commit changes to the database.

        //            return RedirectToAction("Index");   // return to Index.
        //        }



        //        return View(businessUnit);              // else (ModelState is invalid) return view.
        //    }


        //    // GET: BusinessUnit/Edit/5
        //    public ActionResult Edit(int? id)    // GET edit option.
        //    {
        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }

        //        BusinessUnit bu = db.BusinessUnits.Find(id);    // create a variable of type BusinessUnit and set it to the value of id found within the database.
        //        if (bu == null)                                 // if the value is null (no BusinessUnit found).
        //        {
        //            return HttpNotFound();                      // return a HttpNotFound error.
        //        }
        //        return View(bu);                                // if there is no problems with the creation of a new BusinessUnit and one has been found in database with the required value, return the view with this BusinessUnit.
        //    }

        //    // POST: BusinessUnit/Edit/5
        //    [HttpPost]
        //    public ActionResult Edit(BusinessUnit bu)           // POST edit option.
        //    {
        //        try
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                // let ef know that the object state has changed.
        //                db.Entry(bu).State = EntityState.Modified;
        //                // save those changes.
        //                db.SaveChanges();
        //                return RedirectToAction("Index");   // redirected back to index method to see changes.
        //            }
        //            return View(bu);    // if the ModelState is invalid, return back to the view, unchanged.
        //        }
        //        catch
        //        {
        //            //return View(bu);    // if caught, return as something was incorrect.
        //            return RedirectToAction("Index"); // this is the alternative, which returns back to Index's ActionResult, which displays the entire current list.
        //        }
        //    }

        //    // GET: BusinessUnit/Delete/5
        //    public ActionResult Delete(int? id) // get delete option.
        //    {
        //        if (id == null) // validate id is not null.
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }

        //        BusinessUnit businessUnit = db.BusinessUnits.Find(id);  // create a variable of type BusinessUnit and set it to the value of id found within the database.

        //        if (businessUnit == null)                               // check variable is not null.
        //        {
        //            return HttpNotFound();                              // if it is, return a HttpNotFound.
        //        }

        //        return View(businessUnit);                          // else if it passes all checks, return view with businessUnit.
        //    }

        //    // POST: BusinessUnit/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    public ActionResult DeleteConfirmed(int id)
        //    {
        //        BusinessUnit businessUnit = db.BusinessUnits.Find(id);
        //        businessUnit.Active = false;                                // create business unit variable and find id within database collection of BusinessUnits.
        //        db.Entry(businessUnit).State = EntityState.Modified;       // 
        //        db.SaveChanges();                                         // remove businessUnit from the database.                                     
        //                                                                  // save changes to the databases.
        //        return RedirectToAction("Index");                       // return back to Index.
        //    }
        //}



        //// GET: api/SupplierServices
        //public IEnumerable<string> Get()
        //    {
        //        return new string[] { "value1", "value2" };
        //    }

        //    // GET: api/SupplierServices/5
        //    public string Get(int id)
        //    {
        //        return "value";
        //    }

        //    // POST: api/SupplierServices
        //    public void Post([FromBody]string value)
        //    {
        //    }

        //    // PUT: api/SupplierServices/5
        //    public void Put(int id, [FromBody]string value)
        //    {
        //    }

        //    // DELETE: api/SupplierServices/5
        //    public void Delete(int id)
        //    {
        //    }
    }
}
