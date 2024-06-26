﻿using DRX.DTOs;
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
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _inoviceService;
        public InvoiceController(IInvoiceService InoviceService)
        {
            _inoviceService = InoviceService;
        }

        #region Crud Operation

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _inoviceService.GetAllAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("insert")]
        public async Task<IActionResult> Insert([FromBody] InvoiceDTO inovice)
        {
            try
            {
                await CheckRole(inovice);
                return Ok(await _inoviceService.InsertAsync(inovice));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] InvoiceDTO inovice)
        {
            try
            {
                await CheckRole(inovice);
                return Ok(await _inoviceService.UpdateAsync(inovice));
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
               
                return Ok(await _inoviceService.DeleteAsync(new InvoiceDTO { Id = id }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region Other Operation

        [HttpGet("myInvoicedata")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetMyInoviceData()
        {
            try
            {
                var userId = int.Parse(User.FindFirst("Identifier")?.Value);
                return Ok(await _inoviceService.GetMyInvoicesAsync(userId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        #endregion

        #region Private methods
        private async Task CheckRole(InvoiceDTO inovice)
        {
            var userId = int.Parse(User.FindFirst("Identifier")?.Value);
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (!(role == "Admin" || (await _inoviceService.GetUserIdByBilingId(inovice.BilingId)) == userId))
                throw new Exception("You don't have access to modify, view or insert this value");

        }
        #endregion
    }
}
