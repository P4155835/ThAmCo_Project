namespace InventoryManagementModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelInventoryManagement : DbContext
    {
        public ModelInventoryManagement()
            : base("name=ModelInventoryManagement")
        {
        }

        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(e => e.SupplierName)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Ean)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.CategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.BrandName)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Description)
                .IsUnicode(false);
        }
    }
}
