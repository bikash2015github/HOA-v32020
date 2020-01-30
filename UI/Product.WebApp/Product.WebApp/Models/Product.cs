using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.WebApp.Models
{
    public class Product
    {
        public string productId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string manufacturer { get; set; }
        public string type { get; set; }
        public string dateOfManufacturing { get; set; }
        public string dateOfExpiry { get; set; }
    }
}
