using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using ProductAPI.DataAccess;
using ProductAPI.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ProductAPI.Test.DataAccessTest
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class ProductRepositoryTests
    {
        DbContextOptionsBuilder<ProductDBContext> dbContextOptions;
        ProductDBContext dbContext;
        SQLProductRepository repository;

        public ProductRepositoryTests()
        {
            dbContextOptions = new DbContextOptionsBuilder<ProductDBContext>().EnableSensitiveDataLogging().UseInMemoryDatabase(Guid.NewGuid().ToString());
            dbContext = new ProductDBContext(dbContextOptions.Options);            
           
            dbContext.Add(new ProductInfo() { description = "HP Thinkpad", expiryDate = Convert.ToDateTime("2030-06-01"), id = "0002", manufacturer = "HP", manufacturingDate = Convert.ToDateTime("2015-06-01"), name = "Hp Thinkpad", productType = "Laptop" });
            dbContext.Add(new ProductInfo() { description = "Nestle Cofee Classic 200g", expiryDate = Convert.ToDateTime("2019-01-06"), id = "0003", manufacturer = "Nestle", manufacturingDate = Convert.ToDateTime("2019-01-02"), name = "Nestle Cofee", productType = "Cofee" });
            dbContext.Add(new ProductInfo() { description = "Brother Printer L23251", expiryDate = Convert.ToDateTime("2030-06-01"), id = "0004", manufacturer = "Brother", manufacturingDate = Convert.ToDateTime("2015-06-01"), name = "Brother Printer", productType = "Printer" });
            dbContext.SaveChanges();
            repository = new SQLProductRepository(dbContext);
        }

        [TestCase("0005", "Android SmartPhone", "2019-06-21", "Oneplus", "2019-06-21", "Oneplus", "mobile")]
        [Test, Category("ProductRepository")]
        public void Update_Returns_True(string id, string description, string dateOfExpiry, string manufacturer, string dateOfManufacturing, string name, string type)
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
            try
            {
                repository.Add(entity);
                entity.manufacturer = "Oppo";
                var retVal = repository.Update(entity);
                Assert.IsTrue(retVal);
            }
            catch (Exception ex)
            {
                Assert.Fail("Test method :Update_Returns_True, message {0}", ex.Message);
            }

        }

        [TestCase("0001", "Android SmartPhone", "2019-06-21", "Oneplus", "2019-06-21", "Oneplus", "mobile")]
        [Test, Category("ProductRepository")]
        public void Add_Returns_Product(string id, string description, string dateOfExpiry, string manufacturer, string dateOfManufacturing, string name, string type)
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
            try
            {
                ProductInfo addedProduct=repository.Add(entity);
                Assert.IsNotNull(addedProduct);
            }
            catch (Exception ex)
            {
                Assert.Fail("Test method :Add_Returns_Product, message {0}", ex.Message);
            }

        }

        
        [Test, Category("ProductRepository")]
        public void Get_Returns_Product()
        {
            
            try
            {
                List<ProductInfo> products = repository.Get();
                Assert.IsNotNull(products);
                Assert.AreEqual(3, products.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail("Test method :Add_Returns_Product, message {0}", ex.Message);
            }

        }
        
        [TestCase("0002")]
        [Test, Category("ProductRepository")]
        public void Get_Returns_Product_ByProductId(string productId)
        {

            try
            {
                ProductInfo product = repository.Get(productId);
                Assert.IsNotNull(product);
                Assert.AreEqual("HP Thinkpad", product.description);
            }
            catch (Exception ex)
            {
                Assert.Fail("Test method :Get_Returns_Product, message {0}", ex.Message);
            }

        }

        [TestCase("0004")]
        [Test, Category("ProductRepository")]
        public void Delete_Returns_True(string productId)
        {
            try
            {
                var isSuccessfullyDeleted = repository.Delete(productId);                
                Assert.AreEqual(true, isSuccessfullyDeleted);
            }
            catch (Exception ex)
            {
                Assert.Fail("Test method :Get_Returns_Product, message {0}", ex.Message);
            }

        }
    }
}
