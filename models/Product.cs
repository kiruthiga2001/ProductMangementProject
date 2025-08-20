using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public class Product
    {
            [Key]
            public int ProductId { get; set; }

            [Required]
            [StringLength(200)]
            public string Name { get; set; }

            public string Description { get; set; }

            [Required]
            [DataType(DataType.Currency)]
            public decimal Price { get; set; }

            [Required]
            [ForeignKey("Category")]
            public int CategoryId { get; set; }

            public virtual Category Category { get; set; }

            // Navigation property for Attribute Values
            public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; set; }
        }
    }