using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProductAPI.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class QueryParamsInput
    {
        /// <summary>
        /// get or sets the product manufacturer
        /// </summary>
        [DataMember]
        [XmlElement]        
        [MaxLength(30, ErrorMessage = "Product manufacturer can be maximum 30 charactors")]
        public string manufacturer { get; set; }

        /// <summary>
        /// get or sets the product manufacturer type
        /// </summary>
        [DataMember]
        [XmlElement]
        [MaxLength(10, ErrorMessage = "Product type can be maximum 10 charactors")]
        public string type { get; set; }
    }
}
