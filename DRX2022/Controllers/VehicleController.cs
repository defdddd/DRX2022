using DRX.Models;
using DRX.Services.ModelServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DRX2022.Controllers
{
    [Route("DRX2022/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        public VehicleController(IVehicleService VehicleService)
        {
            _vehicleService = VehicleService;
        }

        #region Crud Operation

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _vehicleService.GetAllAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("insert")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Insert([FromBody] VehicleData Vehicle)
        {
            try
            {
                return Ok(await _vehicleService.InsertAsync(Vehicle));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] VehicleData Vehicle)
        {
            try
            {
                return Ok(await _vehicleService.UpdateAsync(Vehicle));
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
                await _vehicleService.DeleteAsync(new VehicleData { Id = id });
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region Other Operation

        [HttpGet("available/{type},{model}")]
        public async Task<IActionResult> GetAvailableVehicles(string type, string model)
        {
            try
            {
                return Ok(await _vehicleService.GetAvailableVehiclesAsync(type, model));
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> SearchById(int id)
        {
            try
            {
                return Ok(await _vehicleService.SearchByIdAsync(id));
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }
        #endregion

        #region Private methods

        #endregion
    }
}
