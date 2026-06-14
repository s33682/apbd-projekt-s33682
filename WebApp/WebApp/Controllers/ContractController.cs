using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTOs;
using WebApp.Exceptions;
using WebApp.Services;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ContractController : ControllerBase
{
    private readonly IContractService _contractService;

    public ContractController(IContractService contractService)
    {
        _contractService = contractService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateContract(PostNewContractDto dto)
    {
        try
        {
            await _contractService.CreateContract(dto);
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

    [HttpPost]
    [Route("payment/{contractId:int}")]
    public async Task<IActionResult> AddContractPayment(int contractId, PostContractPaymentDto dto)
    {
        try
        {
            await _contractService.AddContractPayment(contractId, dto);
            return Created();
        }catch(NotFoundException ex)
        {
            return NotFound(ex.Message);
        }catch(NotPossibleException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}