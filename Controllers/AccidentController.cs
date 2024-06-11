using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Mappy.Services;
using Microsoft.AspNetCore.Authorization;
using Mappy.Models;

namespace Mappy.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AccidentController : ControllerBase
{
  private readonly AccidentService _service;

  public AccidentController(AccidentService service)
  {
      _service = service;
  }

  [HttpGet("")]
  public async Task<IActionResult> GetAllAccidents()
  {
    var id = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.UserData)?.Value;

    if (id is null)
    {
      return BadRequest(new { message = "Cannot get the Id of the user..." });
    }

    var result = await _service.GetAllAsync();

    return Ok(result);
  }

  [HttpPost("")]
  public async Task<IActionResult> ReportAccident(Coordinates coordinates)
  {
    var id = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.UserData)?.Value;

    if (id is null)
    {
      return BadRequest(new { message = "Cannot get the Id of the user..." });
    }

    var result = await _service.AddAsync(new AccidentModel
      {
        UserId = new Guid(id),
        Latitude = coordinates.Latitude,
        Longitude = coordinates.Longitude,
        Date = DateTime.Now
      });

    if (result is null)
    {
      return BadRequest(new { message = "Cannot report the accident!" });
    }
    else
    {
      return Ok(result);
    }
  }
}