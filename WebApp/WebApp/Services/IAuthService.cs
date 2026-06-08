using WebApp.DTOs;

namespace WebApp.Services;

public interface IAuthService
{
    public Task<String> Login(PostLoginDataDto loginData);
}