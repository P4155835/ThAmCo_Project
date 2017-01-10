using InventoryManagementModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using ThAmCo_Project.Dtos;

namespace ThAmCo_Project.ViewModels
{
    public class ProductCompilerViewModel
    {

        private int retryCount = 3; // used for comparing the current retry amount to the set amount of attempts limited, defined here.

        // Get data from the Undercutters service.
        public async Task<IEnumerable<ThAmCoProductDTO>> getUndercuttersProductsAsync()
        {
            int currentRetry = 0;

            for (;;)
            {
                try
                {
                    HttpClient client = createClient("http://undercutters.azurewebsites.net/");
                    Task<HttpResponseMessage> localResponse = client.GetAsync("api/Product");
                    var undercuttersProds = getAsThAmCoProductAsync(await localResponse, "Undercutters"); // grabbing and mapping in method and returning to this variable.
                    client.Dispose();              // disposed from memory.

                    return await undercuttersProds;     // return gathered data, which has been mapped to a ThAmCoProductDTO which can hold all different types of Products from the different suppliers.
                }
                //catch (Exception ex) // use this if "OperationTransientException" is fixed and IsTransient() works as expected.
                catch
                {
                    currentRetry++;

                    // if (currentRetry > this.retryCount || !IsTransient(ex)) // Needs transient handling.
                    if (currentRetry > this.retryCount)
                    {
                        // If this is not a transient error 
                        // or we should not retry re-throw the exception. 
                        //throw;

                        return null; // returns null in this case.
                    }

                    await Task.Delay(5000); // wait 5 seconds
                }
            }
        }

        // Get from Dodgy Dealers service.
        public async Task<IEnumerable<ThAmCoProductDTO>> getDodgyDealersProductsAsync()
        {
            int currentRetry = 0;

            for (; ;)
            {
                try
                {
                    HttpClient client = createClient("http://dodgydealers.azurewebsites.net/");
                    Task<HttpResponseMessage> localResponse = client.GetAsync("api/Product");
                    var dodgyDealersProds = await getAsThAmCoProductAsync(await localResponse, "Dodgy Dealers"); // grabbing and mapping in method and returning to this variable.
                    client.Dispose(); // disposed from memory.

                    return dodgyDealersProds;   // return gathered data, which has been mapped to a ThAmCoProductDTO which can hold all different types of Products from the different suppliers.
                }
                //catch (Exception ex) // use this if "OperationTransientException" is fixed and IsTransient() works as expected.
                catch
                {
                    currentRetry++;

                    // if (currentRetry > this.retryCount || !IsTransient(ex)) // Needs transient handling.
                    if (currentRetry > this.retryCount)
                    {
                        // If this is not a transient error 
                        // or we should not retry re-throw the exception. 
                        //throw;

                        return null; // returns null in this case.
                    }

                    await Task.Delay(5000); // wait 5 seconds
                }
            }   
        }
        
        // Get from Bazzas Bazaar WCF service.
        public async Task<IEnumerable<ThAmCoProductDTO>> getBazzasBazaarProductsAsync()
        {
            int currentRetry = 0;

            for (;;)
            {
                try
                {
                    BazzasBazaarStoreService.StoreClient WCFClient = new BazzasBazaarStoreService.StoreClient();

                    // Gathering all Products from BazzasBazaarStoreService, and mapping to their own DTO.
                    IEnumerable<BazzasBazaarStoreService.Product> BazProducts = await WCFClient.GetFilteredProductsAsync(null, null, null, null);   
                    
                    BazProducts.Select(Bb => new BbProductDTO               
                    {
                        Id = Bb.Id,
                        Ean = Bb.Ean,
                        CategoryId = Bb.CategoryId,
                        CategoryName = Bb.CategoryName,
                        Name = Bb.Name,
                        Description = Bb.Description,
                        PriceForOne = Bb.PriceForOne,
                        PriceForTen = Bb.PriceForTen,
                        InStock = Bb.InStock,
                        ExpectedRestock = Bb.ExpectedRestock,
                    });

                    WCFClient.Close();

                    // Convert retreaved data from Bazzas Bazaar to DTOs from the service, then to ThAmCoProductDTo with all relevant data, and null for irrelevant fields from Undercutters and Dodgy Dealers.
                    var BazzasBazaarToThamCo = BazProducts.Select(Bb => new ThAmCoProductDTO
                    {
                        SupplierProductId = Bb.Id,
                        SupplierName = "Bazzas Bazaar",
                        Ean = Bb.Ean,
                        CategoryId = Bb.CategoryId,
                        CategoryName = Bb.CategoryName,
                        BrandId = null,
                        BrandName = null,
                        Name = Bb.Name,
                        Description = Bb.Description,
                        Price = Bb.PriceForOne, // merging the "PriceForOne" from BazzasBazaar service entry, to the Price value (as this is also for one. Done for convenience).
                        //PriceForOne = Bb.PriceForOne, // could have just merged this to be as Price field instead and take out PriceForOne, but kept this way for consistency with the suppliers formal.
                        PriceForTen = Bb.PriceForTen,
                        InStock = Bb.InStock,
                        ExpectedRestock = Bb.ExpectedRestock,
                    });

                    return BazzasBazaarToThamCo;   // return gathered data, which has been mapped to a ThAmCoProductDTO which can hold all different types of Products from the different suppliers.
                }
                //catch (Exception ex) // use this if "OperationTransientException" is fixed and IsTransient() works as expected.
                catch
                {
                    currentRetry++;

                    // if (currentRetry > this.retryCount || !IsTransient(ex)) // Needs transient handling.
                    if (currentRetry > this.retryCount)
                    {
                        // If this is not a transient error 
                        // or we should not retry re-throw the exception. 
                        //throw;

                        return null; // returns null in this case.
                    }

                    await Task.Delay(5000); // wait 5 seconds
                }
            }
        }

        private bool IsTransient(Exception ex)
        {
            // Determine if the exception is transient.
            // In some cases this may be as simple as checking the exception type, in other 
            // cases it may be necessary to inspect other properties of the exception.

            //if (ex is OperationTransientException)
            //{
            //    return true;
            //}

            var webException = ex as WebException;
            if (webException != null)
            {
                // If the web exception contains one of the following status values 
                // it may be transient.
                return new[] {WebExceptionStatus.ConnectionClosed,
                  WebExceptionStatus.Timeout,
                  WebExceptionStatus.RequestCanceled }
                  .Contains(webException.Status);
            }

            // Additional exception checking logic goes here.
            return false;
        }

        public HttpClient createClient(string uriLink) // function to setup a client, taking an uri input to set the routing.
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(uriLink);
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            return client;
        }

        // method used to gather the DTO shape found in undercutters and dodgyDealers. Could be replaced with automapper, but automapper does what this method is doing, but does not also gather from the two different resources.
        public async Task<IEnumerable<ThAmCoProductDTO>> getAsThAmCoProductAsync(HttpResponseMessage localResponse, string supName)
        {
            IEnumerable<UcDdProductDTO> prodCollection = await localResponse.Content.ReadAsAsync<IEnumerable<UcDdProductDTO>>();

            // Convert retreaved data from undercutters/dodgydealers to DTOs from getUndercuttersProducts(), then to ThAmCoProductDTo with all relevant data, and null for irrelevant fields from BazzasBazaar.
            return prodCollection.Select(Uc => new ThAmCoProductDTO
            //return prods.Select(Uc => new ThAmCoProductDTO
            {
                SupplierProductId = Uc.Id,
                SupplierName = supName,
                Ean = Uc.Ean,
                CategoryId = Uc.CategoryId,
                CategoryName = Uc.CategoryName,
                BrandId = Uc.BrandId,
                BrandName = Uc.BrandName,
                Name = Uc.Name,
                Description = Uc.Description,
                Price = Uc.Price,
                //PriceForOne = null,
                PriceForTen = null,
                InStock = Uc.InStock,
                ExpectedRestock = Uc.ExpectedRestock
            });
        }

    }
}