using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Context;
using WebApp.DTOs;
using WebApp.Exceptions;

namespace WebApp.Services;

public class DbService : IDbService
{
    private readonly AppDbContext _context;

    public DbService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<GetExampleDto> GetExampleById(int id)
    {
        var result = await _context.Examples.FirstOrDefaultAsync(e => e.ExampleId == id);

        if (result == null)
        {
            throw new ExampleException("Example not found");
        }

        return new GetExampleDto{ Id = result.ExampleId, Name = result.Name};
    }
}