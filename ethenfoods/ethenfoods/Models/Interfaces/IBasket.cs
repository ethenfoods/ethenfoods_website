using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ethenfoods.Models.Interfaces
{
    public interface IBasket
    {
        Task<Basket> CreateBasket(Basket basket);
        Basket GetByUserId(string userId);
        Task<Basket> SaveBasket(Basket basket);
        Task<string> Remove(Basket basket);
        List<Basket> GetAllByUserId(string userId);
    }
}
