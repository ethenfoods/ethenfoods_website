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
        public decimal Price { get; set; }
        public int Quantity { get; set; } = 0;
        public string Description { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }

        [Display(Name = "Category")]
        public Category ProductCategory { get; set; }

        [Display(Name = "Bags/Bottles per Case")]
        public int PerCase { get; set; } = 0;

        [Display(Name = "Size per Box")]
        public int PerBox { get; set; } = 0;
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
