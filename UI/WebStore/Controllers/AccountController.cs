﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWebStore.DomainNew.Entities;
using MyWebStore.DomainNew.ViewModels;

namespace MyWebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager, 
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login() => View(new LoginViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            return View(model);

            _logger.LogInformation(new EventId(0, "Login"), $"Попытка входа пользователя {model.UserName} в систему");

            var login_result = await _signInManager.PasswordSignInAsync(
                model.UserName, 
                model.Password, 
                model.RememberMe, 
                false);

            if (login_result.Succeeded)
            {
                _logger.LogInformation(new EventId(1, "Login"), $"Пользователь {model.UserName} удачно вошел в систему");
                if (Url.IsLocalUrl(model.ReturnUrl))
                    return Redirect(model.ReturnUrl);
                return RedirectToAction("Index", "Home");
            }

            _logger.LogWarning(new EventId(1, "Login"), $"Попытка входа пользователя {model.UserName} в систему провалена");
            ModelState.AddModelError("", "Неверный логин или пароль");
            return View(model);

        }

        [HttpGet]
        public IActionResult Register() => View(new RegisterUserViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new User
            {
                UserName = model.UserName
            };

            var registration_result = await _userManager.CreateAsync(user, model.Password);

            if (registration_result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, MyWebStore.DomainNew.Entities.User.UserRole);
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
                foreach (var error in registration_result.Errors)
                    ModelState.AddModelError("", error.Description);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(RegisterUserViewModel model)
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}