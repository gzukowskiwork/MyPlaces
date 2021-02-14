using Identity.Requests;
using Identity.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPlaces.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private IIdentityService _identityService { get; set; }

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("Register")]
        
        public async Task<IActionResult> RegisterAsync([FromBody]RegistrationRequest registrationRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var result = await _identityService.RegisterUserAsync(registrationRequest);
            if(result.Success)
                return StatusCode(200);

            return BadRequest();
        }
    }
}
