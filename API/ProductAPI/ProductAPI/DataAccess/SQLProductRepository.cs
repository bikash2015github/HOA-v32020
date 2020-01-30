using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductAPI.Entities;

namespace ProductAPI.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public class SQLProductRepository : IProductRepository
    {
        #region private
        private readonly ProductDBContext context; 
        #endregion

        #region constructor 
        /// <summary>
        /// 
        /// </summary>
        public SQLProductRepository(ProductDBContext context)
        {
            this.context = context;
        }
        #endregion

        #region public 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public ProductInfo Add(ProductInfo product)
        {
            context.Products.Add(product);
            context.SaveChanges();
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
                ProductInfo product = context.Products.Find(id);
                if (product != null)
                {
                    context.Products.Remove(product);
                    context.SaveChanges();
                }
                retVal = true;
            }
            catch (Exception)
            {
                throw;
            }
            return retVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ProductInfo> Get()
        {
            return context.Products.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductInfo Get(string id)
        {
            return context.Products.Find(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="changedProduct"></param>
        /// <returns></returns>
        public bool Update(ProductInfo changedProduct)
        {
            try
            {
                var product = context.Products.Attach(changedProduct);
                product.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        } 
        #endregion
    }
}
