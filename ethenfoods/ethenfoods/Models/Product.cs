using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ethenfoods.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }

        [Display(Name = "Category")]
        public Category ProductCategory { get; set; }
    }

    public enum Category
    {
        Canned,
        Container,
        Creamer,
        Equipment,
        Fructose,
        Jelly,
        PoppingBoba, 
        Powder,
        Syrup,
        Tapioca, 
        Tea,
    }
}
