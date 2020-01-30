using ProductAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.DataAccess
{
    public interface IProductRepository
    {
        List<ProductInfo> Get();
        ProductInfo Get(string id);
        bool Update(ProductInfo product);
        bool Delete(string id);
        ProductInfo Add(ProductInfo product);

    }
}
