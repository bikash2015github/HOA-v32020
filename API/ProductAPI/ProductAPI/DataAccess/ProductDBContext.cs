using Microsoft.EntityFrameworkCore;
using ProductAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductDBContext : DbContext
    {
        #region private
        /// <summary>
        /// DB Context Private member
        /// </summary>
        private readonly DbContextOptions _dbContextOptions;
        #endregion


        #region constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public ProductDBContext(DbContextOptions<ProductDBContext> options) : base(options)
        {

        }
        #endregion

        #region public
        /// <summary>
        /// 
        /// </summary>
        public DbSet<ProductInfo> Products { get; set; }
        #endregion

        #region protected
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        } 
        #endregion
    }
}
