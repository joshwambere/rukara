using Microsoft.AspNetCore.Mvc;
using superhero.dtos;
using superhero.interfaces;
using superhero.models;
using superhero.services;

namespace superhero.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserDto request)
    {
        var user =await  _userService.RegisterUser(request);
        user.passwordHash = new byte[0];
        user.passwordSalt =  new byte[0];
        user.id = new int();
        return user;

    }
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginDto request)
    {
        Console.WriteLine(request);
        var user = await _userService.LoginUser(request);
        if (user.Token== null)
        {
            return Unauthorized();
        }

        return Ok(user);
    }
}
