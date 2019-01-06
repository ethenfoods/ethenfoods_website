﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ethenfoods.Models;
using ethenfoods.Models.ViewModel;
using ethenfoods.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ethenfoods.Controllers
{
    public class AccountController : Controller
    {

        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signinManager;
        private IBasket _basket;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signinManager, IBasket basket)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _basket = basket;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

    
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signinManager.PasswordSignInAsync(lvm.Email, lvm.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(lvm.Email);

                    //if (await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin))
                    //{
                    //    return RedirectToAction("Index", "Admin");
                    //}

                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError(string.Empty, "Your Credential is Incorrect");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            if (_signinManager.IsSignedIn(User))
            {
                await _signinManager.SignOutAsync();
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel rvm)
        {

            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = rvm.Email,
                    FirstName = rvm.FirstName,
                    LastName = rvm.LastName,
                    Email = rvm.Email,
                    CompanyName = rvm.CompanyName,
                    CompanyAddress = rvm.CompanyAddress,
                    City = rvm.City,
                    State = rvm.State,
                    MemberSince = DateTime.Now
                };

                var result = await _userManager.CreateAsync(user, rvm.Password);

                if (result.Succeeded)
                {
                    await _signinManager.SignInAsync(user, false);

                    Basket newBasket = new Basket();
                    newBasket.UserId = user.Id;
                    await _basket.CreateBasket(newBasket);

                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong, Please try again");
                    return View();
                }

            }

            ModelState.AddModelError(string.Empty, "Your Credential Is Incorrect");
            return View();
        }
    }
}