using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThAmCo_Project.Dtos
{
    public class ThAmCoProductDTO
    {
        // Gets all information, as some information gathered may be needed or required to be recorded. 
        // All of this information can be stored, but not all will be retreivable through the API which will grab from the ThAmCo-InventoryDB. 

        public int? SupplierProductId { get; set; }
        public string SupplierName { get; set; }
        public string Ean { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? BrandId { get; set; }
        public string BrandName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        //public double? PriceForOne { get; set; }
        public double? PriceForTen { get; set; }
        public bool? InStock { get; set; }
        public DateTime? ExpectedRestock { get; set; }
    }
}