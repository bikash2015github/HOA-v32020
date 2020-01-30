using ProductAPI.Managers;
using ProductAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductAPI.Test.ManagersTestData
{
    public class MockProductManager_Exception : IProductManager
    {
        public List<Product> GetProducts(string manufacturer, string productType)
        {
            throw new NotImplementedException();
        }

        public bool UpdateProduct(Product changedProduct)
        {
            throw new NotImplementedException();
        }
    }
}
