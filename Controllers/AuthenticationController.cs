using Mappy.Configurations.Models;
using Mappy.Helpers;
using Mappy.Models;
using Mappy.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Mappy.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
  private readonly UserService _userService;
  private readonly JwtConfigurationModel _jwtBearerTokenSettings;

  public AuthenticationController(IOptions<JwtConfigurationModel> jwtTokenOptions, 
    UserService userService)
  {
    _userService = userService;
    _jwtBearerTokenSettings = jwtTokenOptions.Value;
  }

  [HttpPost("register")]
  public async Task<IActionResult> Register([FromBody] RegisterUserModel model)
  {
    if (!model.Password.Equals(model.ConfirmPassword))
    {
      return new BadRequestObjectResult(new { message = "Passwords do not match!" });
    }

    if (await _userService.ExistByEmail(model.Email))
    {
      return BadRequest(new { message = $"User with email {model.Email} already exist!" });
    }

    var result = await _userService.AddAsync(
      new UserModel {
        Username = model.UserName,
        Email = model.Email,
        Password = AuthenticationHelper.HashPassword(model.Password)
      }
    );

    if (!result)
    {
      return new BadRequestObjectResult(new { message = "Something went wrong with registration!" });
    }

    return Ok(new { message = "User Registration Successful" });
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginUserModel model)
  {
    var user = await _userService.GetAsync(model.Email);

    if (user is null)
    {
      return NotFound(new { message = $"No user with email {model.Email} exist!" });
    }
    else
    {
      if (AuthenticationHelper.HashPassword(model.Password).Equals(user.Password))
      {
        return Ok(new
        {
          token = AuthenticationHelper.GenerateToken(user, _jwtBearerTokenSettings),
          message = "Success",
          user = new { user.Username, user.Email }
        });
      }
      else
      {
        return BadRequest(new { message = "Wrong password!" });
      }
    }
  }
}