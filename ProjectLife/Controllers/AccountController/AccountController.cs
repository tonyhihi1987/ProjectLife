
using Microsoft.AspNetCore.Mvc;
using ProjectLife.DAL;
using AutoMapper;
using ProjectLife.ViewModel;
using ProjectLife.MapperHelper;
using ProjectLife.Model;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;
using System.Globalization;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using ProjectLife.Model.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ProjectLife.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager,UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public async  Task<ActionResult> CreateRole(string roleName)
        {
            var applicationRole = new ApplicationRole
            {
                Name = roleName
            };
           var result =  await _roleManager.CreateAsync(applicationRole);

            if (result.Succeeded)
            {

                ApplicationUser newUser = new ApplicationUser
                {
                    Name = "Chaton",
                    UserName = "Chaton"
                };

                var userResult  =await _userManager.CreateAsync(newUser, "Clement11.");

                if (userResult.Succeeded)
                {
                    var taskRole = _roleManager.FindByNameAsync(roleName);
                    taskRole.Wait();
                    var taskUser = _userManager.FindByNameAsync("Chaton");
                    taskUser.Wait();
                    await _userManager.AddToRoleAsync(taskUser.Result, taskRole.Result.Name);
                }

            }
            return new OkResult();
        }


        public async Task<IActionResult> Login(LogonViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password,true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Nom ou mot de passe invalide");
                    return View(model);
                }
            }
            return View();
        }

    }


}
