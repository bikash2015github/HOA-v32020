using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace ProductAPI.Models
{
    /// <summary>
    /// Product Model, 
    /// used for any request to post the product information
    /// </summary>
    [DataContract(Namespace = "")]
    [ExcludeFromCodeCoverage]
    public class Product
    {
        /// <summary>
        /// get or sets the product id
        /// </summary>
        [DataMember]
        [XmlElement]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Product id is required. ")]
        public string productId { get; set; }

        /// <summary>
        /// get or sets the product name
        /// </summary>
        [DataMember]
        [XmlElement]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Product name is required. ")]
        [MaxLength(30,ErrorMessage ="Product name can be maximum 30 charactors")]
        public string name { get; set; }

        /// <summary>
        /// get or sets the product description
        /// </summary>
        [DataMember]
        [XmlElement]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Product description is required. ")]
        [MaxLength(100, ErrorMessage = "Product description can be maximum 100 charactors")]
        public string description { get; set; }

        /// <summary>
        /// get or sets the product manufacturer
        /// </summary>
        [DataMember]
        [XmlElement]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Product manufacturer is required. ")]
        [MaxLength(30, ErrorMessage = "Product manufacturer can be maximum 30 charactors")]
        public string manufacturer { get; set; }

        /// <summary>
        /// get or sets the product manufacturer type
        /// </summary>
        [DataMember]
        [XmlElement]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Product type is required. ")]
        [MaxLength(10, ErrorMessage = "Product type can be maximum 10 charactors")]
        public string type { get; set; }

        /// <summary>
        /// get or sets the manufacturing date of  a product
        /// </summary>
        [DataMember]
        [XmlElement]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Manufacturing Date Format ('yyyy-MM-dd'). ")]
        [RegularExpression(@"^(\d{4}-[0-1]\d{1}-[0-3]\d{1})$", ErrorMessage = "Invalid Manufacturing Date Format ('yyyy-MM-dd'). ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "dateOfManufacturing is required. ")]
        public string dateOfManufacturing { get; set; }

        /// <summary>
        /// get or sets the expiry date of a product
        /// </summary>
        [DataMember]
        [XmlElement]
        [DataType(DataType.Date, ErrorMessage = "Invalid Expiry Date Format ('yyyy-MM-dd'). ")]
        [RegularExpression(@"^(\d{4}-[0-1]\d{1}-[0-3]\d{1})$", ErrorMessage = "Invalid Expiry Date Format ('yyyy-MM-dd'). ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "dateOfExpiry is required. ")]
        public string dateOfExpiry { get; set; }

    }
}