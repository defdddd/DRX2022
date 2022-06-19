using DRX.Models;
using DRX.Services.AuthService;
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
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
      
        // POST api/<AuthController>
        [HttpPost]
        public async Task<IActionResult> Create(AuthData authData)
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
        public async Task<IActionResult> Register([FromBody] UserData user)
        {
            try
            {
                return Ok(await _authService.RegisterAsync(user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
