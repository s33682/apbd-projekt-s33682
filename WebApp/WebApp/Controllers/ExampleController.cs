using Microsoft.AspNetCore.Mvc;
using WebApp.Exceptions;
using WebApp.Services;

namespace WebApp.Controllers;


[ApiController]
[Route("example/[controller]")]
public class ExampleController : ControllerBase
{
    private readonly IDbService _dbService;

    public ExampleController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet]
    [Route("{exmaple:int}")]
    public async Task<IActionResult> Get(int exmaple)
    {
        try
        {
            var result = await _dbService.GetExampleById(exmaple);
            return Ok(result);
        }catch(ExampleException e)
        {
            return BadRequest(e.Message);
        }
    }
}