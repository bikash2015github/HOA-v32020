using Moq;
using NUnit.Framework;
using ProductAPI.DataAccess;
using ProductAPI.Entities;
using ProductAPI.Managers;
using ProductAPI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ProductAPI.Test.ManagersTest
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class ProductManagerTests
    {
        Mock<IProductRepository> mockObject = null;
        private List<ProductInfo> _products;
        public ProductManagerTests()
        {
            _products = new List<ProductInfo>()
            {
                new ProductInfo(){ description="Android SmartPhone",expiryDate=Convert.ToDateTime("2019-06-21"),id="0001",manufacturer="Oneplus",manufacturingDate=Convert.ToDateTime("2019-06-21"),name="Oneplus",productType="mobile"},
                new ProductInfo(){ description="HP Thinkpad",expiryDate=Convert.ToDateTime("2030-06-01"),id="0002",manufacturer="HP",manufacturingDate=Convert.ToDateTime("2015-06-01"),name="Hp Thinkpad",productType="Laptop"},
                new ProductInfo(){ description="Nestle Cofee Classic 200g",expiryDate=Convert.ToDateTime("2019-01-06"),id="0003",manufacturer="Nestle",manufacturingDate=Convert.ToDateTime("2019-01-02"),name="Nestle Cofee",productType="Cofee"},
                new ProductInfo(){ description="Brother Printer L23251",expiryDate=Convert.ToDateTime("2030-06-01"),id="0004",manufacturer="Brother",manufacturingDate=Convert.ToDateTime("2015-06-01"),name="Brother Printer",productType="Printer"}
            };
        }

        [TestCase("0001", "Android SmartPhone", "2019-06-21", "Oneplus", "2019-06-21", "Oneplus", "mobile")]
        [Test, Category("ProductManager")]
        public void UpsertProduct_Returns_True(string id, string description, string dateOfExpiry, string manufacturer, string dateOfManufacturing, string name, string type)
        {
            //set the entity
            ProductInfo entity = new ProductInfo()
            {
                description = description,
                expiryDate = Convert.ToDateTime(dateOfExpiry),
                id = id,
                manufacturer = manufacturer,
                manufacturingDate = Convert.ToDateTime(dateOfManufacturing),
                name = name,
                productType = type
            };
            //set the model
            Product model = new Product()
            {
                description = description,
                dateOfExpiry = dateOfExpiry,
                productId = id,
                manufacturer = manufacturer,
                dateOfManufacturing = dateOfManufacturing,
                name = name,
                type = type
            };

            try
            {
                //Mock setup
                mockObject = new Mock<IProductRepository>();
                mockObject.Setup(item => item.Update(It.IsAny<ProductInfo>())).Returns(true);

                //calling the updateproduct method
                ProductManager manager = new ProductManager(mockObject.Object);
                var retVal = manager.UpdateProduct(model);
                Assert.IsTrue(retVal);
            }
            catch (Exception ex)
            {
                Assert.Fail("Test method :UpsertProduct_Success failed, message {0}", ex.Message);
            }

        }

        [TestCase("0001", "Android SmartPhone", "2019-06-21", "Oneplus", "2019-06-21", "Oneplus", "mobile")]
        [Test, Category("ProductManager")]
        public void UpsertProduct_Throws_Exception(string id, string description, string dateOfExpiry, string manufacturer, string dateOfManufacturing, string name, string type)
        {
            //set the entity
            ProductInfo entity = new ProductInfo()
            {
                description = description,
                expiryDate = Convert.ToDateTime(dateOfExpiry),
                id = id,
                manufacturer = manufacturer,
                manufacturingDate = Convert.ToDateTime(dateOfManufacturing),
                name = name,
                productType = type
            };
            //set the model
            Product model = new Product()
            {
                description = description,
                dateOfExpiry = dateOfExpiry,
                productId = id,
                manufacturer = manufacturer,
                dateOfManufacturing = dateOfManufacturing,
                name = name,
                type = type
            };

            try
            {
                //Mock setup
                mockObject = new Mock<IProductRepository>();
                mockObject.Setup(item => item.Update(It.IsAny<ProductInfo>())).Throws(new Exception("DB Connection failed to open"));

                //calling the updateproduct method
                ProductManager manager = new ProductManager(mockObject.Object);
                var retVal = manager.UpdateProduct(model);
                Assert.IsNull(retVal);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "DB Connection failed to open");
            }

        }

        [TestCase("MOBILE","ONEPLUS")]
        [TestCase("MOBILE", "")]
        [TestCase("", "ONEPLUS")]
        [Test, Category("ProductManager")]
        public void GetProducts_Returns_FilteredItem(string productType,string manufacturer)
        {          
            try
            {
                //Mock setup
                mockObject = new Mock<IProductRepository>();
                mockObject.Setup(item => item.Get()).Returns(_products);


                //calling the updateproduct method
                ProductManager manager = new ProductManager(mockObject.Object);
                var retVal = manager.GetProducts(manufacturer, productType);
                Assert.IsNotNull(retVal,"Expecting not null but it returns null");
                Assert.AreEqual(1, retVal.Count, $"Expecting count 1 but it returns {retVal.Count}");
            }
            catch (Exception ex)
            {                
                Assert.Fail("Test method :UpsertProduct_Success failed, message {0}", ex.Message);
            }
        }

        [TestCase("", "")]
        [Test, Category("ProductManager")]
        public void GetProducts_Returns_AllItem(string productType, string manufacturer)
        {
            try
            {
                //Mock setup
                mockObject = new Mock<IProductRepository>();
                mockObject.Setup(item => item.Get()).Returns(_products);


                //calling the updateproduct method
                ProductManager manager = new ProductManager(mockObject.Object);
                var retVal = manager.GetProducts(productType, manufacturer);
                Assert.IsNotNull(retVal, "Expecting not null but it returns null");
                Assert.AreEqual(4, retVal.Count, $"Expecting count 4 but it returns {retVal.Count}");
            }
            catch (Exception ex)
            {
                Assert.Fail("Test method :UpsertProduct_Success failed, message {0}", ex.Message);
            }
        }

        [TestCase("MOBILE", "ONEPLUS")]
        [TestCase("MOBILE", "")]
        [TestCase("", "ONEPLUS")]
        [Test, Category("ProductManager")]
        public void GetProducts_Throws_Exception(string productType, string manufacturer)
        {
            try
            {
                //Mock setup
                mockObject = new Mock<IProductRepository>();
                mockObject.Setup(item => item.Get()).Throws(new Exception("DB connection failed to open"));


                //calling the updateproduct method
                ProductManager manager = new ProductManager(mockObject.Object);
                var retVal = manager.GetProducts(productType, manufacturer);
                Assert.IsNull(retVal);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("DB connection failed to open", ex.Message);
            }
        }
    }
}
