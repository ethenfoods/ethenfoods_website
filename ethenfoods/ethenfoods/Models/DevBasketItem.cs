using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ethenfoods.Models.Interfaces;
using ethenfoods.Data;

namespace ethenfoods.Models
{
    public class DevBasketItem : IBasketItem
    {
        private EthenFoodsDbContext _context;

        public DevBasketItem(EthenFoodsDbContext context)
        {
            _context = context;
        }

        public async Task<BasketItem> CreateItem(BasketItem basketItem)
        {
            await _context.BasketItems.AddAsync(basketItem);
            await _context.SaveChangesAsync();
            return basketItem;
        }

        public async Task<BasketItem> UpdateItem(BasketItem basketItem)
        {
            _context.BasketItems.Update(basketItem);
            await _context.SaveChangesAsync();

            return basketItem;
        }

        public BasketItem GetItemById(int id)
        {
            var basketItem = _context.BasketItems.FirstOrDefault(i => i.ID == id);
            return basketItem;
        }

        public BasketItem GetItemByProductId(int basketId, int productId)
        {
            var basketItem = _context.BasketItems
                                     .Where(item => item.BasketId == basketId)
                                     .FirstOrDefault(item => item.ProductId == productId);
            return basketItem;
        }

        public List<BasketItem> GetItemByBasketId(int basketId)
        {
            var basketItems = _context.BasketItems.Where(i => i.BasketId == basketId).ToList();
            return basketItems;
        }
    }
}
