using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApp.Context;
using WebApp.DTOs;
using WebApp.Exceptions;
using WebApp.Models;

namespace WebApp.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }
    
    public async Task<String> Login(PostLoginDataDto loginData)
    {
        var passwordHasher = new PasswordHasher<Employee>();
        
        var result = await _context.Employees.Include(employee => employee.Role).FirstOrDefaultAsync(e => e.Login == loginData.Login);
        
        if (result == null)
        {
            throw new AuthException("Login is incorrect!");
        }
        
        var verifyResult = passwordHasher.VerifyHashedPassword(result, result.Password, loginData.Password);

        if (verifyResult == PasswordVerificationResult.Failed)
        {
            throw new AuthException("Password is incorrect!");
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, result.Login),
            new Claim(ClaimTypes.Role, result.Role.RoleName),
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );
        
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenString;
    }
}