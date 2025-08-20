using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public class ProductAttributeValue
    {
        [Key]
        public int ProductAttributeValueId { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [Required]
        [ForeignKey("AttributeValue")]
        public int AttributeValueId { get; set; }

        public virtual Product Product { get; set; }
        public virtual AttributeValue AttributeValue { get; set; }
    }
}