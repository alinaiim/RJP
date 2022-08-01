using Microsoft.AspNetCore.Mvc;
using RJP.API.Dtos;
using RJP.Application.AccountUseCases;

namespace RJP.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IOpenAccountService _openAccountService;
    public CustomerController(IOpenAccountService openAccountService)
    {
        _openAccountService = openAccountService;
    }

    [HttpPost]
    public async Task<IActionResult> OpenAccount(CustomerReadDto dto)
    {
        var response = await _openAccountService.Execute(dto.CustomerId, dto.InitialCredit);
        if (response.Success)
            return Ok();
        else if (response.Message == "Customer not found") return NotFound(response.Message);
        return BadRequest();
    }
}
