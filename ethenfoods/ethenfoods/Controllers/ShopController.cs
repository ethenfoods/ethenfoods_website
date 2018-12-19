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
        private UserManager<ApplicationUser> _userManager;
        private IProduct _product;
        private IBasket _basket;
        private IBasketItem _basketItems;

        public ShopController(UserManager<ApplicationUser> userManager, 
                              IProduct product, 
                              IBasket basket, 
                              IBasketItem basketItems)
        {
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

            ShoppingCartViewModel scvm = new ShoppingCartViewModel();
            scvm.Basket = basket;
            scvm.BasketItems = basketItems;

            return View(scvm);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var basket = _basket.GetByUserId(user.Id);

            if ( _basketItems.GetItemByProductId(basket.ID, id) == null)
            {
                var selectedProduct = _product.GetById(id);
                BasketItem basketItem = new BasketItem
                {
                    BasketId = basket.ID,
                    ProductId = id,
                    Product = await selectedProduct,
                    Quantity = 1
                };

                await _basketItems.CreateItem(basketItem);
            }

            else
            {
                var basketItem = _basketItems.GetItemByProductId(basket.ID, id);
                basketItem.Quantity += 1;
                await _basketItems.UpdateItem(basketItem);
            }

            return RedirectToAction("ProductList");
        }
    }
}
