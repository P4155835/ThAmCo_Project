using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThAmCo_WebServiceAPI.Dtos
{
    public class ThAmCoProductDTO
    {
        // edited slightly from the same DTO in the inventory manager project,
        // as only some information is wanted to be visible by public from the DB.

        public int? ThAmCo_Id { get; set; }
        public string Ean { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
       // public double? PriceForOne { get; set; }
        public double? PriceForTen { get; set; }
        public bool? InStock { get; set; }
        public DateTime? ExpectedRestock { get; set; }
    }
}