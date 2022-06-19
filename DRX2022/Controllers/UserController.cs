using DRX.Models;
using DRX.Services.ModelServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DRX2022.Controllers
{
    [Route("DRX2022/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        #region Crud Operation

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _userService.GetAllAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("insert")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Insert([FromBody] UserData user)
        {
            try
            {
                return Ok(await _userService.InsertAsync(user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Update([FromBody] UserData user)
        {
            try
            {
                await CheckRole(user);

                return Ok(await _userService.UpdateAsync(user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                
                return Ok(await _userService.DeleteAsync(new UserData { Id = id }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region Other Operation
        [HttpGet("username/{userName}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> SearchByUserName(string userName)
        {
            try
            {
                var person = await _userService.SearchByUserNameAsync(userName);
                await CheckRole(person);
                return Ok(person);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("myProfile")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetMyData()
        {
            try
            {
                var userId = int.Parse(User.FindFirst("Identifier")?.Value);
                var person = await _userService.SearchByIdAsync(userId);
                await CheckRole(person);
                return Ok(person);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpGet("email/{email}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> SearchByEmail(string email)
        {
            try
            {
                var person = await _userService.SearchByEmailAsync(email);
                await CheckRole(person);
                return Ok(person);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region Private methods
        private async Task CheckRole(UserData user)
        {
            var userId = int.Parse(User.FindFirst("Identifier")?.Value);
            var userData = await _userService.SearchByIdAsync(user.Id);
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (userId.Equals(user.Id) && !userData.IsAdmin.Equals(user.IsAdmin))
                throw new Exception("You can't edit your role, contact the owner for this task");

            if (!(role == "Admin" || user.Id == userId))
                throw new Exception("You don't have access to modify, view or insert this value");

        }
        #endregion
    }
}
