using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTOs;
using WebApp.Exceptions;
using WebApp.Services;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SubscriptionController : ControllerBase
{
    private readonly ISubsriptionService _subscriptionService;

    public SubscriptionController(ISubsriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }
    
    
    [HttpPost]
    public async Task<IActionResult> CreateSubscription(PostCreateSubscriptionDto dto)
    {
        try
        {
            await _subscriptionService.CreateSubscription(dto);
            return Created();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (AlreadyDoneException ex)
        {
            return Conflict(ex.Message);
        }
        catch (NotPossibleException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("{subscriptionId:int}/payment")]
    public async Task<IActionResult> AddSubscriptionPayment(int subscriptionId, PostSubscriptionPaymentDto dto)
    {
        try
        {
            await _subscriptionService.AddSubscriptionPayment(subscriptionId, dto);
            return Created();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }catch(NotPossibleException ex)
        {
            return BadRequest(ex.Message);
        }catch(AlreadyDoneException ex)
        {
            return Conflict(ex.Message);
        }
    }
}