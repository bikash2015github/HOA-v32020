using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.Entities
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// 
    /// </summary>
    public static class ModelBuilderExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public static void Seed(this ModelBuilder model)
        {
            model.Entity<ProductInfo>().HasData(
                 new ProductInfo() { description = "Android SmartPhone", expiryDate = Convert.ToDateTime("2019-06-21"), id = "0001", manufacturer = "Oneplus", manufacturingDate = Convert.ToDateTime("2019-06-21"), name = "Oneplus", productType = "mobile" },
                new ProductInfo() { description = "HP Thinkpad", expiryDate = Convert.ToDateTime("2030-06-01"), id = "0002", manufacturer = "HP", manufacturingDate = Convert.ToDateTime("2015-06-01"), name = "Hp Thinkpad", productType = "Laptop" },
                new ProductInfo() { description = "Nestle Cofee Classic 200g", expiryDate = Convert.ToDateTime("2019-01-06"), id = "0003", manufacturer = "Nestle", manufacturingDate = Convert.ToDateTime("2019-01-02"), name = "Nestle Cofee", productType = "Cofee" },
                new ProductInfo() { description = "Brother Printer L23251", expiryDate = Convert.ToDateTime("2030-06-01"), id = "0004", manufacturer = "Brother", manufacturingDate = Convert.ToDateTime("2015-06-01"), name = "Brother Printer", productType = "Printer" }
                );
        }
    }
}
