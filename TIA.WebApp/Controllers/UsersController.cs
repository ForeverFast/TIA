using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TIA.Core.AspNetCoreEntities;
using TIA.WebApp.Models;

namespace TIA.WebApp.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("Users")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            List<UserViewModel> userList = new List<UserViewModel>();
            foreach (User user in _userManager.Users.ToList())
            {
                UserViewModel uVm = new UserViewModel()
                {
                    Id = user.Id,
                    Year = user.Year,
                    Email = user.Email,
                    Roles = _userManager.GetRolesAsync(user).Result
                };
                userList.Add(uVm);
            }

            return View(userList);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create() => View();

        [HttpPost]
        [Route("Create/{model?}")]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email, Year = model.Year };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        [Route("Edit/{id?}")]
        public async Task<IActionResult> Edit(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel { Id = user.Id, Email = user.Email, Year = user.Year };
            return View(model);
        }

        [HttpPost]
        [Route("Edit/{model?}")]
        public async Task<IActionResult> Edit([FromForm] EditUserViewModel model)
        {
           
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.Year = model.Year;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        [Route("Delete/{id?}")]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IList<string> roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("admin"))
                    return BadRequest("Нельзя удалить админа.");

                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
    }
}
