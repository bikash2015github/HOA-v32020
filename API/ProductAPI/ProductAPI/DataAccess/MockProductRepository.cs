using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using ProductAPI.Entities;

namespace ProductAPI.DataAccess
{
    /// <summary>
    /// Mock product repository that use in memoery data
    /// </summary>
    [ExcludeFromCodeCoverage]
   public class MockProductRepository : IProductRepository
    {
        #region private members
        private List<ProductInfo> _products;
        #endregion

        #region constructor
        /// <summary>
        /// 
        /// </summary>
        public MockProductRepository()
        {
            _products = new List<ProductInfo>()
            {
                new ProductInfo(){ description="Android SmartPhone",expiryDate=Convert.ToDateTime("2019-06-21"),id="0001",manufacturer="Oneplus",manufacturingDate=Convert.ToDateTime("2019-06-21"),name="Oneplus",productType="mobile"},
                new ProductInfo(){ description="HP Thinkpad",expiryDate=Convert.ToDateTime("2030-06-01"),id="0002",manufacturer="HP",manufacturingDate=Convert.ToDateTime("2015-06-01"),name="Hp Thinkpad",productType="Laptop"},
                new ProductInfo(){ description="Nestle Cofee Classic 200g",expiryDate=Convert.ToDateTime("2019-01-06"),id="0003",manufacturer="Nestle",manufacturingDate=Convert.ToDateTime("2019-01-02"),name="Nestle Cofee",productType="Cofee"},
                new ProductInfo(){ description="Brother Printer L23251",expiryDate=Convert.ToDateTime("2030-06-01"),id="0004",manufacturer="Brother",manufacturingDate=Convert.ToDateTime("2015-06-01"),name="Brother Printer",productType="Printer"}
            };
        }

        #endregion

        #region public methods
        /// <summary>
        ///  
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public ProductInfo Add(ProductInfo product)
        {
            product.id = _products.Max(i => i.id) + 1;
            _products.Add(product);
            return product;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            bool retVal = false;
            try
            {
                ProductInfo product = _products.FirstOrDefault(p => p.id.Equals(id));
                if (product != null)
                {
                    _products.Remove(product);
                }
                retVal = true;
            }
            catch (Exception ex)
            {
                //log
            }
            return retVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ProductInfo> Get()
        {
            return _products;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductInfo Get(string id)
        {
            return _products.FirstOrDefault(p => p.id.Equals(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="changedProduct"></param>
        /// <returns></returns>
        public bool Update(ProductInfo changedProduct)
        {
            bool retVal = false;
            try
            {
                ProductInfo product = _products.FirstOrDefault(p => p.id.Equals(changedProduct.id));
                if (product != null)
                {
                    product.manufacturer = changedProduct.manufacturer;
                    product.expiryDate = changedProduct.expiryDate;
                    product.name = changedProduct.name;
                    product.productType = changedProduct.productType;
                    product.description = changedProduct.description;
                    product.manufacturingDate = changedProduct.manufacturingDate;

                    retVal = true;
                }                
            }
            catch (Exception ex)
            {
                //log
            }
            return retVal;
        }
        #endregion
    }
}
