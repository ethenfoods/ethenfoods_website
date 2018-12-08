using System;
using ethenfoods.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ethenfoods.Models.Interfaces
{
    public interface IProduct
    {
        Task<string> Create(Product product);
        Task<string> Remove(int id);
        Task<List<Product>> GetAll();
        Task<Product> GetById(int id);
        Task<List<Product>> GetByCategory(Category category);
        Task<string> Update(Product product);
    }
}
