using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTOs;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(PostLoginDataDto loginData)
    {
        try
        {
            var token =  await _authService.Login(loginData);
            return Ok(token);
        }catch(Exception e)
        {
            return Unauthorized(e.Message);
        }
    }
    
    /*[HttpGet("generate-hash")]
    public IActionResult GenerateHash(string password)
    {
        var hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<Employee>();
        return Ok(hasher.HashPassword(null!, password));
    }*/
}