using Microsoft.AspNetCore.Mvc;
using RJP.API.Dtos;
using RJP.Application.AccountUseCases;

namespace RJP.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IOpenAccountService _openAccountService;
    private readonly IShowAccountsInfoService _showAccountsInfoService;
    public CustomerController(IOpenAccountService openAccountService, IShowAccountsInfoService showAccountsInfoService)
    {
        _openAccountService = openAccountService;
        _showAccountsInfoService = showAccountsInfoService;
    }

    [HttpPost]
    public async Task<IActionResult> OpenAccount(CustomerReadDto dto)
    {
        var response = await _openAccountService.Execute(dto.CustomerId, dto.InitialCredit);
        if (response.Success)
        {
            AccountResponseDto accountResponse = new AccountResponseDto()
            {
                AccountId = response.Payload.AccountId,
                FirstName = response.Payload.Customer.FirstName,
                LastName = response.Payload.Customer.LastName,
                InitialCredit = response.Payload.InitialCredit
            };
            return Ok(accountResponse);
        }
        else if (response.Message == "Customer not found") return NotFound(response.Message);
        return BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> GetAccountsInformation(int customerId)
    {
        var response = await _showAccountsInfoService.Execute(customerId);
        if (response.Success)
        {
            IEnumerable<AccountResponseDto> accounts = response.Payload.Select(a => new AccountResponseDto()
            {
                AccountId = a.AccountId,
                FirstName = a.Customer.FirstName,
                InitialCredit = a.InitialCredit,
                LastName = a.Customer.LastName
            });
            return Ok(accounts);
        }
        else if (response.Message == "Customer not found") return NotFound(response.Message);
        return BadRequest();
    }
}
