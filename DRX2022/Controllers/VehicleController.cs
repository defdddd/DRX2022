using DRX.DTOs;
using DRX.Services.ModelServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Linq;


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
        public async Task<IActionResult> Insert([FromBody] VehicleDTO Vehicle)
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
        public async Task<IActionResult> Update([FromBody] VehicleDTO Vehicle)
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
                return Ok(await _vehicleService.DeleteAsync(new VehicleDTO { Id = id }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region Other Operation



        #region Counts

        [HttpGet("availableCount")]
        public async Task<IActionResult> AvailableCount()
        {
            try
            {
                var result = await _vehicleService.GetAllAvailableVehiclesAsync();
                return Ok(result.Count());
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }


        [HttpGet("AllCount")]
        public async Task<IActionResult> GetAvailableVehicles()
        {
            try
            {
                var result = await _vehicleService.GetAllAsync();
                return Ok(result.Count());
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }

        [HttpGet("AllElectricCount")]
        public async Task<IActionResult> GetAllElectricCount()
        {
            try
            {
                var result = await _vehicleService.GetAllAsync();
                return Ok(result.Count( x => x.Model.Equals("Electric")));
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }

        [HttpGet("AllBikesCount")]
        public async Task<IActionResult> GetAllBikesCount()
        {
            try
            {
                var result = await _vehicleService.GetAllAsync();
                return Ok(result.Count(x => x.Type.Equals("Bike")));
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }

        [HttpGet("AllCarCount")]
        public async Task<IActionResult> GetAllCarCount()
        {
            try
            {
                var result = await _vehicleService.GetAllAsync();
                return Ok(result.Count(x => x.Type.Equals("Car")));
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }

        #endregion




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

        [HttpGet("searchBy/{type},{model}")]
        public async Task<IActionResult> GetAllSearchByVehicles(string type, string model)
        {
            try
            {
                if (type.Equals("n")) type = null;
                if (model.Equals("n")) model = null;

                return Ok(await _vehicleService.GetAllSearchByVehiclesAsync(type, model));
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
