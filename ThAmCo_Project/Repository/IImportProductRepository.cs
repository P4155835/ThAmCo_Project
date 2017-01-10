using InventoryManagementModel;
using System.Collections.Generic;
using ThAmCo_Project.Dtos;

namespace ThAmCo_Project.Repository
{
    public interface IImportProductRepository
    {
        void Insert(IEnumerable<ThAmCoProductDTO> prodCollection, string supplierName, List<string> registeredSuppliers);
        void Update(Product thisProd, Product prod); 
        void Save();
    }
}
