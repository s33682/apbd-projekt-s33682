using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Exceptions;
using WebApp.Services;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RevenueController : ControllerBase
{
    private readonly IRevenueService _revenueService;

    public RevenueController(IRevenueService revenueService)
    {
        _revenueService = revenueService;
    }

    [HttpGet]
    [Route("actual")]
    public async Task<IActionResult> GetActualRevenue(int? softwareId, string? currency)
    {
        try
        {
            var result = await _revenueService.GetActualRevenue(softwareId, currency);
            return Ok(result);
        }
        catch(NotFoundException ex)
        {
            return NotFound(ex.Message);
        }catch(NotPossibleException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet]
    [Route("predicted")]
    public async Task<IActionResult> GetPredictedRevenue(int? softwareId, string? currency)
    {
        try
        {
            var result = await _revenueService.GetPredictedRevenue(softwareId, currency);
            return Ok(result);
        }
        catch(NotFoundException ex)
        {
            return NotFound(ex.Message);
        }catch(NotPossibleException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}