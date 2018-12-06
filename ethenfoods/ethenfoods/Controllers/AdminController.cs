using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ethenfoods.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ethenfoods.Controllers
{
    public class AdminController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult ProductPage()
        {
            return View();
        }

        public IActionResult ProductDetail()
        {
            return View();
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