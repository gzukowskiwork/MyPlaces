using Identity.Response;
using Microsoft.AspNetCore.Identity;
using MyPlaces.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationContext _applicationContext;

        public IdentityService(UserManager<ApplicationUser> userManager, ApplicationContext applicationContext)
        {
            _userManager = userManager;
            _applicationContext = applicationContext;
        }

        public async Task<RegistrationResponse> RegisterUserAsync(string email, string password)
        {
            ApplicationUser applicationUser = await _userManager.FindByEmailAsync(email);
            if (applicationUser != null)
            {
                return new RegistrationResponse
                {
                    Success = false,
                    Errors = new[] { string.Format($"Users with email {email} exists", email) }
                };
            }

            ApplicationUser newUser = new ApplicationUser
            {
                Email = email,
                UserName = email
            };

            IdentityResult createdUser = await _userManager.CreateAsync(newUser, password);

            if (!createdUser.Succeeded)
            {
                return new RegistrationResponse
                {
                    Errors = createdUser.Errors.Select(e=>e.Description)
                };
            }
            throw new NotImplementedException();
        }
    }
}
