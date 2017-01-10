﻿using System;

namespace ThAmCo_Project.Dtos
{
    public class UcDdProductDTO
    {
        public int? Id { get; set; }
        public string Ean { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? BrandId { get; set; }            
        public string BrandName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public bool? InStock { get; set; }
        public DateTime? ExpectedRestock { get; set; }
    }
}