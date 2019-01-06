using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ethenfoods.Models.Interfaces
{
    public interface IBasketItem
    {
        Task<BasketItem> CreateItem(BasketItem basketItem);
        Task<BasketItem> UpdateItem(BasketItem basketItem);
        BasketItem GetItemById(int id);
        BasketItem GetItemByProductId(int basketId, int productId);
        List<BasketItem> GetItemByBasketId(int basketId);
    }
}
