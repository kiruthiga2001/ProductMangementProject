using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public class AttributeValue
    {
        [Key]
        public int AttributeValueId { get; set; }

        [Required]
        [StringLength(100)]
        public string Value { get; set; }

        [Required]
        [ForeignKey("Attribute")]
        public int AttributeId { get; set; }

        public virtual Attribute Attribute { get; set; }
    }
}