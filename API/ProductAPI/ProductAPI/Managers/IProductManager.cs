using ProductAPI.Models;
using System.Collections.Generic;

namespace ProductAPI.Managers
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProductManager
    {
        /// <summary>
        /// Get the products filtered by product manufacturer and / or product type, if both the parameters are empty , it returns all the products
        /// </summary>
        /// <param name="manufacturer">product manufacturer</param>
        /// <param name="productType"> product type</param>
        /// <returns></returns>
        List<Product> GetProducts(string manufacturer, string productType);
        /// <summary>
        /// update the products depends on product id
        /// </summary>
        /// <param name="changedProduct">the product information that will be updated</param>
        /// <returns><see cref="bool"/></returns>
        bool UpdateProduct(Product changedProduct);
    }
}