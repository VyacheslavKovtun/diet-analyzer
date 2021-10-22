using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    public class AdminController : Controller
    {
        UserManager<IdentityUser> _userManager;
        RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<IdentityUser> uManager, RoleManager<IdentityRole> rManager)
        {
            _userManager = uManager;
            _roleManager = rManager;

            /*_roleManager.CreateAsync(new IdentityRole("Admin")).Wait();

            var user1 = _userManager.FindByEmailAsync("kovtyn11191@gmail.com").Result;
            var user2 = _userManager.FindByEmailAsync("kovtun.v.work@gmail.com").Result;

            var adminRole = _roleManager.FindByNameAsync("Admin").Result;
            _userManager.AddToRoleAsync(user1, adminRole.ToString()).Wait();
            _userManager.AddToRoleAsync(user2, adminRole.ToString()).Wait();*/
        }

        public async Task<IActionResult> Index()
        {
            return View(await _userManager.GetUsersInRoleAsync("Admin"));
        }
    }
}
