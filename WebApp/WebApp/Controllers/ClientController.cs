using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTOs;
using WebApp.Exceptions;
using WebApp.Services;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpPost]
    [Route("individual")]
    public async Task<IActionResult> AddIndividualClient(PostNewIndividualClientDto dto)
    {
        try
        {
            await _clientService.AddIndividualClient(dto);
            return Created();
        }
        catch (AlreadyDoneException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPost]
    [Route("company")]
    public async Task<IActionResult> AddCompanyClient(PostNewCompanyClientDto dto)
    {
        try
        {
            await _clientService.AddCompanyClient(dto);
            return Created();
        }
        catch (AlreadyDoneException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpDelete]
    [Route("{clientId:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteClient(int clientId)
    {
        try
        {
            await _clientService.DeleteClient(clientId);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (AlreadyDoneException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (NotPossibleException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPut]
    [Route("{clientId:int}/individual")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateIndividualClient(int  clientId, PutUpdateIndividualClientDto dto)
    {
        try
        {
            await _clientService.UpdateIndividualClient(clientId, dto);
            return NoContent();
        }catch(NotFoundException ex)
        {
            return NotFound(ex.Message);
        }catch(NotPossibleException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPut]
    [Route("{clientId:int}/company")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateCompanyClient(int clientId, PutUpdateCompanyClientDto dto)
    {
        try
        {
            await _clientService.UpdateCompanyClient(clientId, dto);
            return NoContent();
        }catch(NotFoundException ex)
        {
            return NotFound(ex.Message);
        }catch(NotPossibleException ex)
        {
            return Conflict(ex.Message);
        }
    }
}