using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductAPI.DataAccess;
using ProductAPI.Entities;
using ProductAPI.Models;
using Serilog;

namespace ProductAPI.Managers
{
    /// <summary>
    ///  Contains all the differents business logic after retrieving products from DB
    /// </summary>
    public class ProductManager : IProductManager
    {
        #region Private Variables
        private readonly IProductRepository productRepository;
        #endregion

        #region constructors
        /// <summary>
        ///  Initializes a new instance of the <see cref="ProductManager" /> class.
        /// </summary>
        /// <param name="productRepository"></param>
        public ProductManager(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        #endregion

        #region Private Methods

        /// <summary>
        ///  Get products filtered by product type
        /// </summary>
        /// <param name="productType"></param>
        /// <returns></returns>
        private List<Product> GetProductFilteredByProductType(string productType)
        {
            List<Product> lstProduct = new List<Product>();
            List<ProductInfo> lstProductInfo = new List<ProductInfo>();
            try
            {
                lstProductInfo = productRepository.Get();
                if (lstProductInfo != null)
                {
                    lstProductInfo = lstProductInfo.Where(item => item.productType.ToLower().Equals(productType.ToLower())).ToList();

                    foreach (ProductInfo productInfo in lstProductInfo)
                    {
                        Product product = new Product();
                        ConvertEntityToModel(productInfo, product);
                        lstProduct.Add(product);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception while calling GetProductFilteredByProductType()");
                throw;
            }
            return lstProduct;
        }

        /// <summary>
        /// Get the product filtered by product manufacturer
        /// </summary>
        /// <param name="manufacturer"></param>
        /// <returns></returns>
        private List<Product> GetProductFilteredByManufacturer(string manufacturer)
        {
            List<Product> lstProduct = new List<Product>();
            List<ProductInfo> lstProductInfo = new List<ProductInfo>();
            try
            {
                lstProductInfo = productRepository.Get();
                if (lstProductInfo != null)
                {
                    lstProductInfo = lstProductInfo.Where(item => item.manufacturer.ToLower().Equals(manufacturer.ToLower())).ToList();

                    foreach (ProductInfo productInfo in lstProductInfo)
                    {
                        Product product = new Product();
                        ConvertEntityToModel(productInfo, product);
                        lstProduct.Add(product);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception while calling GetProductFilteredByManufacturer()");
                throw;
            }
            return lstProduct;
        }
        /// <summary>
        ///  Get the products filtered by manufacturer and product type
        /// </summary>
        /// <param name="manufacturer"></param>
        /// <param name="productType"></param>
        /// <returns></returns>
        private List<Product> GetProductFilteredByManufacturerandProductType(string manufacturer, string productType)
        {
            List<Product> lstProduct = new List<Product>();
            List<ProductInfo> lstProductInfo = new List<ProductInfo>();
            try
            {
                lstProductInfo = productRepository.Get();
                if (lstProductInfo != null)
                {
                    lstProductInfo = lstProductInfo.
                                                Where(item => item.manufacturer.ToLower().Equals(manufacturer.ToLower())
                                                                        && item.productType.ToLower().Equals(productType.ToLower()))
                                                                        .ToList();

                    foreach (ProductInfo productInfo in lstProductInfo)
                    {
                        Product product = new Product();
                        ConvertEntityToModel(productInfo, product);
                        lstProduct.Add(product);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception while calling GetProductFilteredByManufacturerandProductType()");
                throw;
            }
            return lstProduct;
        } 
        #endregion

        #region public methods
        /// <summary>
        /// update the products depends on product id
        /// </summary>
        /// <param name="changedProduct">the product information that will be updated</param>
        /// <returns>bool</returns>
        public bool UpdateProduct(Product changedProduct)
        {
            bool retVal = false;
            try
            {
                ProductInfo product = new ProductInfo();
                ConvertModelToEntity(changedProduct, product);
                retVal = productRepository.Update(product);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception while calling UpdateProduct()");
                throw;
            }
            return retVal;
        }

        /// <summary>
        /// Get the products filtered by product manufacturer and / or product type, if both the parameters are empty , it returns all the products
        /// </summary>
        /// <param name="manufacturer">product manufacturer</param>
        /// <param name="productType"> product type</param>
        /// <returns></returns>
        public List<Product> GetProducts(string manufacturer, string productType)
        {
            List<Product> lstProduct = new List<Product>();
            List<ProductInfo> lstProductInfo = new List<ProductInfo>();
            try
            {
                if (string.IsNullOrWhiteSpace(manufacturer) && !string.IsNullOrWhiteSpace(productType))
                {
                    lstProduct = GetProductFilteredByProductType(productType);
                }
                else if (!string.IsNullOrWhiteSpace(manufacturer) && string.IsNullOrWhiteSpace(productType))
                {
                    lstProduct = GetProductFilteredByManufacturer(manufacturer);
                }
                else if (!string.IsNullOrWhiteSpace(manufacturer) && !string.IsNullOrWhiteSpace(productType))
                {
                    lstProduct = GetProductFilteredByManufacturerandProductType(manufacturer, productType);
                }
                else
                {
                    lstProductInfo = productRepository.Get();

                    foreach (ProductInfo productInfo in lstProductInfo)
                    {
                        Product product = new Product();
                        ConvertEntityToModel(productInfo, product);
                        lstProduct.Add(product);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception while calling GetProducts()");
                throw;
            }
            return lstProduct;
        } 
        #endregion

        #region Utility
        /// <summary>
        /// Convert entity to model
        /// </summary>
        /// <param name="prodInfo"></param>
        /// <param name="product"></param>
        public void ConvertEntityToModel(ProductInfo prodInfo, Product product)
        {
            product.productId = prodInfo.id;
            product.name = prodInfo.name;
            product.manufacturer = prodInfo.manufacturer;
            product.type = prodInfo.productType;
            product.description = prodInfo.description;
            product.dateOfManufacturing = prodInfo.manufacturingDate.ToString("yyyy-MM-dd");
            product.dateOfExpiry = prodInfo.expiryDate.ToString("yyyy-MM-dd");

        }

        /// <summary>
        /// Convert model to entity
        /// </summary>
        /// <param name="model"></param>
        /// <param name="entity"></param>
        public void ConvertModelToEntity(Product model, ProductInfo entity)
        {
            entity.id = model.productId;
            entity.name = model.name;
            entity.manufacturer = model.manufacturer;
            entity.productType = model.type;
            entity.description = model.description;
            entity.manufacturingDate = Convert.ToDateTime(model.dateOfManufacturing);
            entity.expiryDate = Convert.ToDateTime(model.dateOfExpiry);

        }
        #endregion
    }
}
