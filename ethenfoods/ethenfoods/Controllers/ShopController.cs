using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ethenfoods.Data;
using ethenfoods.Models;
using ethenfoods.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ethenfoods.Models.Interfaces;

namespace ethenfoods.Controllers
{
    public class ShopController : Controller
    {
        private IConfiguration _configuration;
        private UserManager<ApplicationUser> _userManager;
        private IProduct _product;
        private IBasket _basket;
        private IBasketItem _basketItems;

        public ShopController(IConfiguration Configuration, 
                              UserManager<ApplicationUser> userManager, 
                              IProduct product, 
                              IBasket basket, 
                              IBasketItem basketItems)
        {
            _configuration = Configuration;
            _userManager = userManager;
            _product = product;
            _basket = basket;
            _basketItems = basketItems;
        }

        public async Task<IActionResult> ProductList()
        {
            var products = await _product.GetAll();
            ProductListViewModel plvm = new ProductListViewModel();
            plvm.Products = products;
            return View(plvm);
        }

        public async Task<IActionResult> ProductByCategory(Category category)
        {
            var products = await _product.GetByCategory(category);
            ProductListViewModel plvm = new ProductListViewModel();
            plvm.Products = products;
            return View(plvm);
        }

        public async Task<IActionResult> ProductDetail(int id)
        {
            var product = await _product.GetById(id);
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> ShoppingCart()
        {
            var user = await _userManager.GetUserAsync(User);
            if (_basket.GetByUserId(user.Id) == null)
            {
                Basket newBasket = new Basket
                {
                    UserId = user.Id
                };
                await _basket.CreateBasket(newBasket);
            }

            var basket = _basket.GetByUserId(user.Id);

            List<BasketItem> basketItems = _basketItems.GetItemByBasketId(basket.ID);
            if (basketItems != null)
            {
                foreach (BasketItem item in basketItems)
                {
                    Product product = await _product.GetById(item.ProductId);
                    item.Product = product;
                }
            }
            basket.BasketItems = basketItems;

            return View(basket);
        }
    }
}
