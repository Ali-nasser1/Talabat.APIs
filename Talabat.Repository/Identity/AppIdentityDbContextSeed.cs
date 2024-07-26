using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task UserSeedAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var User = new AppUser()
                {
                    DisplayName = "Ali Eid",
                    Email = "ali.eid@gmail.com",
                    UserName = "ali.eid",
                    PhoneNumber = "01123456789"
                };
                await userManager.CreateAsync(User, "P@ssw0rd");
            }
        }
    }
}
