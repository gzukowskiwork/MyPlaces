using Identity.Requests;
using Identity.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service
{
    public interface IIdentityService
    {
        Task<RegistrationResponse> RegisterUserAsync(RegistrationRequest registrationRequest);
    }
}
