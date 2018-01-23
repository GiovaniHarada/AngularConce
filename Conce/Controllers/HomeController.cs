using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Conce.Core.Models;
using Conce.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Conce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ConceContext _ctx;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(ConceContext ctx, UserManager<AppUser> userManager)
        {
            this._ctx = ctx;
            this._userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }

        [Route("api/defaultUser")]
        [HttpGet]
        public async Task<IActionResult> Seed()
        {
            await _ctx.Database.EnsureCreatedAsync();
            var user = await _userManager.FindByEmailAsync("giovaniharada@gmail.com");
            if (user == null)
            {
                user = new AppUser()
                {
                    FirstName = "Giovani",
                    LastName = "Harada",
                    UserName = "giovaniharada@gmail.com",
                    Email = "giovaniharada@gmail.com"
                };
                var result = await _userManager.CreateAsync(user, "@!Giovani425");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Failed to create default user");
                }
            }
            return Ok("DefaultUserCreated");
        }
    }
}
