using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities.Identity;
namespace Talabat.APIs.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<AppUser?> FindUserWithAddressAsync(this UserManager<AppUser> userManager, ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users.Where(u => u.Email == email).Include(u => u.Address).FirstOrDefaultAsync();
            return user;
        }
    }
}
