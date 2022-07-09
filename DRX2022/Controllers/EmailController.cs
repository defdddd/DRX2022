using DRX.Services.EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DRX2022.Controllers
{
    [Route("DRX2022/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
             _emailService = emailService;
        }

        [HttpGet("forgotPasswordToken/{email}/{code}")]
        public async Task<IActionResult> ForgotPasswordToken(string email, string code)
        {
            try
            {
                return Ok(await _emailService.GetTokenForForgotPasswordAsync(email, code));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

      
        [HttpGet("rent")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Appointment()
        {
            try
            {
                var userId = int.Parse(User.FindFirst("Identifier")?.Value);
                await _emailService.SendRentMadeEmailAsync(userId);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("finished")]
        [Authorize(Roles = "Admin,User")]

        public async Task<IActionResult> Finished()
        {
            try
            {
                var userId = int.Parse(User.FindFirst("Identifier")?.Value);
                await _emailService.SendRentFinishedEmailAsync(userId);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("forgotPasswordSend")]
        public async Task<IActionResult> ForgotPasswordSend([FromBody] string emailTo)
        {
            try
            {
                await _emailService.SendForgotPasswordEmailAsync(emailTo);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
