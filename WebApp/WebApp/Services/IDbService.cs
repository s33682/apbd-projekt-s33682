using WebApp.DTOs;

namespace WebApp.Services;

public interface IDbService
{
    public Task<GetExampleDto> GetExampleById(int id);
}