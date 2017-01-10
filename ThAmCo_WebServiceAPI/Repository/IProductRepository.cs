using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThAmCo_WebServiceAPI.Dtos;

namespace ThAmCo_WebServiceAPI.Repository
{
    public interface IProductRepository
    {
        // could update to be generic, should this service require expanding and thus handling more requests and class types.

        IEnumerable<ThAmCoProductDTO> SelectAll();
        //IEnumerable<ThAmCoProductDTO> SelectAll(int? id);
        ThAmCoProductDTO SelectById(int id);
        
        //void Insert(ThAmCoProductDTO obj); //needed?
        //void Update(ThAmCoProductDTO obj); //needed?
        //void Delete(string id); //needed?
       //void Save(); //needed?
    }
}
