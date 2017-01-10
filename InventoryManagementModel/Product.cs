namespace InventoryManagementModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [Key]
        public int ThAmCo_Id { get; set; }

        public string SupplierName { get; set; }

        public int? SupplierProductId { get; set; }

        public string Ean { get; set; }

        public int? CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int? BrandId { get; set; }

        public string BrandName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double? Price { get; set; }

        public double? PriceForTen { get; set; }

        public bool? InStock { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ExpectedRestock { get; set; }
    }
}
