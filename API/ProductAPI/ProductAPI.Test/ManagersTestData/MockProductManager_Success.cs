using ProductAPI.Managers;
using ProductAPI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ProductAPI.Test.ManagersTestData
{
    [ExcludeFromCodeCoverage]
    public class MockProductManager_Success : IProductManager
    {
        List<Product> lstProduct = null;
        public MockProductManager_Success()
        {
            lstProduct = new List<Product>()
            {
                new Product(){ description="Android SmartPhone",dateOfExpiry="2019-06-21",productId="0001",manufacturer="Oneplus",dateOfManufacturing="2019-06-21",name="Oneplus",type="mobile"},
                new Product(){ description="HP Thinkpad",dateOfExpiry="2030-06-01",productId="0002",manufacturer="HP",dateOfManufacturing="2015-06-01",name="Hp Thinkpad",type="Laptop"},
                new Product(){ description="Nestle Cofee Classic 200g",dateOfExpiry="2019-01-06",productId="0003",manufacturer="Nestle",dateOfManufacturing="2019-01-02",name="Nestle Cofee",type="Cofee"},
                new Product(){ description="Brother Printer L23251",dateOfExpiry="2030-06-01",productId="0004",manufacturer="Brother",dateOfManufacturing="2015-06-01",name="Brother Printer",type="Printer"}
            };
        }
        private List<Product> GetProducts()
        {

            return lstProduct;
        }

        private List<Product> GetProductFilteredByManufacturer(string manufacturer)
        {
            return lstProduct.FindAll(item => item.manufacturer.ToLower().Equals(manufacturer.ToLower()));
        }

        private List<Product> GetProductFilteredByManufacturerandProductType(string manufacturer, string productType)
        {
            return lstProduct.FindAll(item => item.manufacturer.ToLower().Equals(manufacturer.ToLower()) && item.type.ToLower().Equals(productType.ToLower()));
        }

        private List<Product> GetProductFilteredByProductType(string productType)
        {
            return lstProduct.FindAll(item => item.type.ToLower().Equals(productType.ToLower()));
        }

        public bool UpdateProduct(Product changedProduct)
        {
            return true;
        }

        public List<Product> GetProducts(string manufacturer, string productType)
        {
            if (string.IsNullOrWhiteSpace(manufacturer) && !string.IsNullOrWhiteSpace(productType))
            {
               return GetProductFilteredByProductType(productType);
            }
            else if (!string.IsNullOrWhiteSpace(manufacturer) && string.IsNullOrWhiteSpace(productType))
            {
               return GetProductFilteredByManufacturer(manufacturer);
            }
            else if (!string.IsNullOrWhiteSpace(manufacturer) && !string.IsNullOrWhiteSpace(productType))
            {
               return GetProductFilteredByManufacturerandProductType(manufacturer,productType);
            }
            else
            {
                return GetProducts();
            }
        }
    }
}
