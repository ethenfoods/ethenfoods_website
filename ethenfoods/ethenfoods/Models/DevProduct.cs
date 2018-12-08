using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ethenfoods.Data;
using ethenfoods.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ethenfoods.Models
{
	public class DevProduct : IProduct
	{
        private EthenFoodsDbContext _context;
		
        public DevProduct(EthenFoodsDbContext context)
        {
            _context = context;
        }

        public async Task<string> Create(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return "complete";
        }

        public async Task<string> Remove(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ID == id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            if(product == null)
            {
                return "complete";
            }
            else
            {
                return "failed";
            }
        }

        public async Task<List<Product>> GetAll()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return products;
        }

        public async Task<Product> GetById(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ID == id);
            return product;
        }

        public async Task<List<Product>> GetByCategory(Category category)
        {
            List<Product> products = await _context.Products.Where(p => p.ProductCategory == category).ToListAsync();
            return products;
        }

        public async Task<string> Update(Product product)
        {
            var dbProduct = await _context.Products.FirstOrDefaultAsync(p => p.ID == product.ID);

            if(dbProduct.ID == product.ID)
            {
                dbProduct.SKU = product.SKU;
                dbProduct.Name = product.Name;
                dbProduct.Price = product.Price;
                dbProduct.Quantity = product.Quantity;
                dbProduct.Description = product.Description;
                dbProduct.Image = product.Image;
                dbProduct.ProductCategory = product.ProductCategory;

                _context.Products.Update(dbProduct);
                await _context.SaveChangesAsync();
                return "complete";
            }
            else
            {
                return "failed";
            }

        }
	}
}
