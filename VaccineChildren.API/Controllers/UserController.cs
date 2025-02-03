﻿using Microsoft.AspNetCore.Mvc;
using VaccineChildren.Application.DTOs.Request;
using VaccineChildren.Application.DTOs.Response;
using VaccineChildren.Application.Services;
using VaccineChildren.Core.Base;

namespace VaccineChildren.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : BaseController
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserReq userReq)
    {
        try
        {
            var userRes = await _userService.Login(userReq);
            return Ok(BaseResponse<UserRes>.OkResponse(userRes, "Login successful"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error at login: {Message}", ex.Message);
            return HandleException(ex, nameof(Login));
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        try
        {
            await _userService.RegisterUserAsync(registerRequest);
            return Ok(BaseResponse<string>.OkResponse(mess: "User registered successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error at register: {Message}", ex.Message);
            return HandleException(ex, nameof(Register));
        }
    }
    
}