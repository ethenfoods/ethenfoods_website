using ethenfoods.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ethenfoods.Models.Interfaces;

namespace ethenfoods.Models
{
    public class DevBasket : IBasket
    {
        private EthenFoodsDbContext _context;

        public DevBasket(EthenFoodsDbContext context)
        {
            _context = context;
        }

        public async Task<Basket> CreateBasket(Basket basket)
        {
            await _context.Baskets.AddAsync(basket);
            await _context.SaveChangesAsync();
            return basket;
        }

        public Basket GetByUserId(string userId)
        {
            var basket = _context.Baskets.Where(b => b.UserId == userId)
                                         .FirstOrDefault(b => b.IsComplete == false);
            return basket;
        }

        public async Task<Basket> SaveBasket(Basket basket)
        {
            _context.Baskets.Update(basket);
            await _context.SaveChangesAsync();
            return basket;
        }

        public async Task<string> Remove(Basket basket)
        {
            _context.Baskets.Remove(basket);
            await _context.SaveChangesAsync();
            return "complete";
        }

        public List<Basket> GetAllByUserId(string userId)
        {
            var baskets = _context.Baskets.Where(b => b.UserId == userId)
                                          .ToList();
            return baskets;
        }
    }
}
