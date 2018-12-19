using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ethenfoods.Models.ViewModel
{
    public class ShoppingCartViewModel
    {
        public Basket Basket {get; set;}
        public List<BasketItem> BasketItems { get; set; }
    }
}
