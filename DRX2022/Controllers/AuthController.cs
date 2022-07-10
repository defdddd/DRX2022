using DRX.DTOs;
using DRX.Services.AuthService;
using DRX.Services.EmailService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DRX2022.Controllers
{
    [Route("DRX2022/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;
        public AuthController(IAuthService authService, IEmailService emailService)
        {
            _authService = authService;
            _emailService = emailService;
        }
      
        // POST api/<AuthController>
        [HttpPost]
        public async Task<IActionResult> Create(AuthDTO authData)
        {
            try
            {
                return Ok(await _authService.GenerateTokenAsync(authData));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO user)
        {
            try
            {
                var result = await _authService.RegisterAsync(user);

                await _emailService.SendCreatedEmailAsync(result.Email);

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

       

        [HttpGet("checkLogin")]
        public IActionResult Check()
        {
            try
            {
                var userId = int.Parse(User.FindFirst("Identifier")?.Value);

                if (userId > 0) return Ok(true);
            }
            catch { }

            return Ok(false);

        }
    }
}
