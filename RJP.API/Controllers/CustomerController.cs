using Microsoft.AspNetCore.Mvc;
using RJP.API.Dtos;

namespace RJP.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    [HttpGet]
    public IActionResult OpenAccount([FromQuery]CustomerReadDto dto)
    {
        return Ok(dto);
    }
}
