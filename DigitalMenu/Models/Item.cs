using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DigitalMenu.Models
{
    public class Item 
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Contents { get; set; }
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        [Display(Name = "Special Text")]
        public string SpecialText { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price1 { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price2 { get; set; }
        public short Rank { get; set; }
        public byte[] Image { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string MemberId { get; set; }
    }
}