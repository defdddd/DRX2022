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
    public class BilingController : ControllerBase
    {
        private readonly IBilingService _BilingService;
        public BilingController(IBilingService BilingService)
        {
            _BilingService = BilingService;
        }

        #region Crud Operation

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _BilingService.GetAllAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("insert")]
        public async Task<IActionResult> Insert([FromBody] BilingData Biling)
        {
            try
            {
                CheckRole(Biling);
                return Ok(await _BilingService.InsertAsync(Biling));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] BilingData Biling)
        {
            try
            {
                CheckRole(Biling);
                return Ok(await _BilingService.UpdateAsync(Biling));
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
                
                return Ok(await _BilingService.DeleteAsync(new BilingData { Id = id }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region Other Operation

        [HttpGet("myBilingdata")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetMyBilingData()
        {
            try
            {
                var userId = int.Parse(User.FindFirst("Identifier")?.Value);
                return Ok(await _BilingService.GetBilingByUserIdAsync(userId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        #endregion

        #region Private methods
        private void CheckRole(BilingData Biling)
        {
            var userId = int.Parse(User.FindFirst("Identifier")?.Value);
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (!(role == "Admin" || Biling.UserId == userId))
                throw new Exception("You don't have access to modify, view or insert this value");

        }
        #endregion
    }
}
