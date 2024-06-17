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
  private readonly AccidentService _accidentService;
  private readonly UserService _userService;

  public AccidentController(AccidentService service, UserService userService)
  {
      _accidentService = service;
      _userService = userService;
  }

  [HttpGet("")]
  public async Task<IActionResult> GetAllAccidents()
  {
    var id = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.UserData)?.Value;

    if (id is null)
    {
      return BadRequest(new { message = "Cannot get the Id of the user..." });
    }

    var result = await _accidentService.GetAllAsync();

    var recentAccidents = result.Where(accident => accident.Date >= DateTime.Now.AddDays(-7));

    return Ok(recentAccidents);
  }

  [HttpPost("")]
  public async Task<IActionResult> ReportAccident(Coordinates coordinates)
  {
    var id = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.UserData)?.Value;

    if (id is null)
    {
      return BadRequest(new { message = "Cannot get the Id of the user..." });
    }

    var user = await _userService.GetAsync(new Guid(id));

    if (user.LastReportedAccidentDate != default && 
      user.LastReportedAccidentDate >= DateTime.Now.AddMinutes(-5)) {
        return BadRequest(new { message = "You can report only one accident every 5 minutes!" });
    }

    await _userService.UpdateLastReportedAccidentDate(new Guid(id));

    var result = await _accidentService.AddAsync(new AccidentModel
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