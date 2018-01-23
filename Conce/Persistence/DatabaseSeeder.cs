using Conce.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conce.Persistence
{
    public class DatabaseSeeder
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ConceContext _ctx;

        public DatabaseSeeder(UserManager<AppUser> userManager, ConceContext ctx)
        {
            this._userManager = userManager;
            this._ctx = ctx;
        }
        public async Task Seed()
        {
            await _ctx.Database.EnsureCreatedAsync();
            var user = await _userManager.FindByEmailAsync("giovaniharada@gmail.com");
            if(user == null)
            {
                user = new AppUser() {
                    FirstName = "Giovani",
                    LastName = "Harada",
                    UserName = "giovaniharada@gmail.com",
                    Email = "giovaniharada@gmail.com"
                };
                var result = await _userManager.CreateAsync(user, "@!Giovani425");
                if(result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Failed to create default user");
                }
            }

        }
    }
}
