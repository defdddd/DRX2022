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
    public class RentController : ControllerBase
    {
        private readonly IRentService _RentService;
        public RentController(IRentService RentService)
        {
            _RentService = RentService;
        }

        #region Crud Operation

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _RentService.GetAllAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("insert")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Insert([FromBody] RentData Rent)
        {
            try
            {
                CheckRole(Rent);
                return Ok(await _RentService.InsertAsync(Rent));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] RentData Rent)
        {
            try
            {
                return Ok(await _RentService.UpdateAsync(Rent));
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
               
                return Ok(await _RentService.DeleteAsync(new RentData { Id = id }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region Other Operation

        [HttpGet("getMyRents")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetMyRents()
        {
            try
            {
                var userId = int.Parse(User.FindFirst("Identifier")?.Value);
                return Ok(await _RentService.GetMyRentsAsync(userId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region Private methods
        private void CheckRole(RentData Rent)
        {
            var userId = int.Parse(User.FindFirst("Identifier")?.Value);
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (!(role == "Admin" || Rent.UserId == userId))
                throw new Exception("You don't have access to modify, view or insert this value");

        }
        #endregion
    }
}
