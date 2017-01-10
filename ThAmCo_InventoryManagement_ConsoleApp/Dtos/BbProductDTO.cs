using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThAmCo_InventoryManagement_ConsoleApp.Dtos
{
    class BbProductDTO
    {
        public int? Id { get; set; }
        public string Ean { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? PriceForOne { get; set; }
        public double? PriceForTen { get; set; }
        public bool? InStock { get; set; }
        public DateTime? ExpectedRestock { get; set; }
    }
}
