using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TIA.BusinessLogicLayer.WebInterface;
using TIA.WebInterface.Models;

namespace TIA.WebInterface.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly ITiaModelWebInterface _tiaModel;

        public AccountController(ITiaModelWebInterface tiaModel)
        {
            _tiaModel = tiaModel;
        }

        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Register(RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
        //        if (user == null)
        //        {
        //            // добавляем пользователя в бд
        //            db.Users.Add(new User { Email = model.Email, Password = model.Password });
        //            await db.SaveChangesAsync();

        //            await Authenticate(model.Email); // аутентификация

        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //            ModelState.AddModelError("", "Некорректные логин и(или) пароль");
        //    }
        //    return View(model);
        //}

        [HttpGet]
        [Route("Login/{returnUrl?}")]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Login/{model?}")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var client = new RestClient("http://localhost:18062/Account/token");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AlwaysMultipartFormData = true;
                    request.AddParameter("username", model.Email);
                    request.AddParameter("password", model.Password);
                    IRestResponse response = client.Execute(request);

                    var tVm = JsonConvert.DeserializeObject<JsonCoreObject<TokenViewModel>>(response.Content).Object;

                    if (!string.IsNullOrEmpty(tVm.AccessToken) )
                    {
                        await Authenticate(tVm); // аутентификация

                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
                catch (Exception)
                {
                    return View(model);
                }
                
            }
            return View(model);
        }

        private async Task Authenticate(TokenViewModel tokenViewModel)
        {
            _tiaModel.Token = tokenViewModel.AccessToken;

            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, tokenViewModel.UserName),
                new Claim("token", tokenViewModel.AccessToken),
                new Claim("refreshToken", tokenViewModel.RefreshToken)
            };
            
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie");
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [HttpPost]
        [Authorize]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
