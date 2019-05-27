using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DigitalMenu.Models
{
    public class Category

    {

        public Category()
        {
            this.Items = new HashSet<Item>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(70)]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Category Description")]
        public string CategoryDescription { get; set; }
        public short Rank { get; set; }
        [Display(Name = "Special Text")]
        public string SpecialText { get; set; }

        public virtual ICollection<Item> Items { get; set; }

        public string MemberId { get; set; }
        //[Display(Name = "Is Active")]
        //public bool IsActive { get; set; }
    }
}