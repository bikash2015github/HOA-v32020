using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductInfo
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public DateTime manufacturingDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public DateTime expiryDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [MaxLength(10)]
        public string productType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string manufacturer { get; set; }
    }
}
