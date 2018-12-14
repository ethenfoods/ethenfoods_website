using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ethenfoods.Models
{
    public class Basket
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsComplete { get; set; } = false;
    }
}
