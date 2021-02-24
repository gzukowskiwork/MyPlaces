using EmailService;
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
        private readonly IIdentityService _identityService;
        private readonly IEmailEmitter _emailEmitter;

        public IdentityController(IIdentityService identityService, IEmailEmitter emailEmitter)
        {
            _identityService = identityService;
            _emailEmitter = emailEmitter;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegistrationRequest registrationRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var result = await _identityService.RegisterUserAsync(registrationRequest);
            if (result.Success)
            {
                await _emailEmitter.SendMailAsync(new Email(registrationRequest.Email, "Woelcome", "dupa dupa dupa"));
                return StatusCode(200);
            }
            return BadRequest();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _identityService.LoginAsync(loginRequest);

            if (result.Success)
                return StatusCode(200, result.Token);
            return BadRequest();
        }
    }
}
