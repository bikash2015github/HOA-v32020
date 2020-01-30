using ProductAPI.Managers;
using ProductAPI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ProductAPI.Test.ManagersTestData
{
    [ExcludeFromCodeCoverage]
    public class MockProductManager_Empty : IProductManager
    {
        public bool UpdateProduct(Product changedProduct)
        {
            return true;
        }

        public List<Product> GetProducts(string manufacturer, string productType)
        {
           return null;
        }
    }
}
