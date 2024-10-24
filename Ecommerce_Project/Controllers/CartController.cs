﻿using Ecommerce_Project.DTOs.Cart;
using Ecommerce_Project.Models;
using Ecommerce_Project.Services.CartServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Project.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICartServices _cartServices;

        public CartController(UserManager<ApplicationUser> userManager, ICartServices cartServices)
        {
            _userManager = userManager;
            _cartServices = cartServices;
        }

        public async Task<IActionResult> Index()
        {
            //Get the current user
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var userCart = await _cartServices.GetCartsByUserId(user);

                if (userCart.Data.Count > 0)
                {
                    return View(userCart.Data.Where(x => x.Complete == false).ToList());
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AddCart(AddCartDTO newCart)
        {
            //Get the current user
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                newCart.ApplicationUser = user;
                await _cartServices.AddCart(newCart);
            }

			return RedirectToAction("Index", "Home");
        }

		public async Task<IActionResult> UpdateCart(UpdateCartDTO updatedCart)
		{
			//Get the current user
			ApplicationUser user = await _userManager.GetUserAsync(User);

			if (user != null)
			{
				var cart = await _cartServices.GetCartById(updatedCart.Id);
                if (cart.Data != null) 
                { 
                    if(cart.Data.ApplicationUser == user)
                    {
						await _cartServices.UpdateCart(updatedCart);
					}    
                }
				
			}

			return RedirectToAction("Index", "Cart");
		}

        public async Task<IActionResult> CancellCart(int id)
        {
            //Get the current user
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                var cart = await _cartServices.GetCartById(id);
                if (cart.Data != null)
                {
                    if (cart.Data.ApplicationUser == user)
                    {
                        await _cartServices.CancelCartById(id);
                    }
                }
            }
            return RedirectToAction("Index", "Cart");
        }
    }
}
