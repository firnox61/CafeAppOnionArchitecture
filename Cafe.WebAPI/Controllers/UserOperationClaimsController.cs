﻿using Cafe.Application.DTOs.Users;
using Cafe.Application.Interfaces.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationClaimsController : ControllerBase
    {
        private readonly IUserOperationClaimService _userOperationClaimService;

        public UserOperationClaimsController(IUserOperationClaimService userOperationClaimService)
        {
            _userOperationClaimService = userOperationClaimService;
        }

        // 🔹 Kullanıcıya rol atama
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UserOperationClaimCreateDto dto)
        {
            var result = await _userOperationClaimService.AddAsync(dto);
            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }

        // 🔹 Belirli kullanıcıya ait roller
        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var result = await _userOperationClaimService.GetByUserIdAsync(userId);
            return result.Success ? Ok(result.Data) : NotFound(result.Message);
        }

        // 🔹 Kullanıcıdan rolü kaldırma (silme)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userOperationClaimService.DeleteAsync(id);
            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }
    }
}
