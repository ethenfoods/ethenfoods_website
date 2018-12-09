using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ethenfoods.Models;
using ethenfoods.Models.Interfaces;
using ethenfoods.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ethenfoods.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private IProduct _product;

        public AdminController(UserManager<ApplicationUser> userManager, IProduct product)
        {
            _userManager = userManager;
            _product = product;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public async Task<IActionResult> ProductList()
        {
            var products = await _product.GetAll();
            ProductListViewModel plvm = new ProductListViewModel();
            plvm.Products = products;
            return View(plvm);
        }

        [HttpGet]
        public IActionResult ProductCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(Product product)
        {
            if (ModelState.IsValid)
            {
                await _product.Create(product);
                return RedirectToAction("ProductList");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> ProductUpdate(int id)
        {
            var product = await _product.GetById(id);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductUpdate(Product product)
        {
            if (ModelState.IsValid)
            {
                await _product.Update(product);
                return RedirectToAction("ProductList");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                var requestedProduct = await _product.GetById(product.ID);
                return View(requestedProduct);
            }
        }

        public async Task<IActionResult> ProductRemove(int id)
        {
            string result = await _product.Remove(id);
            return RedirectToAction("ProductList");
        }

        public IActionResult UserList()
        {
            return View();
        }

        public IActionResult UserDetail()
        {
            return View();
        }
    }
}