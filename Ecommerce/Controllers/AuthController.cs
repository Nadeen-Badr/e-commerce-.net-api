using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ECommerceApi.Models;
using ECommerceApi.DTOs;
using ECommerceApi.Helpers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
    {
        var user = new User { UserName = registerDTO.Email, Email = registerDTO.Email, Role = registerDTO.Role };
        var result = await _userManager.CreateAsync(user, registerDTO.Password);

        if (result.Succeeded)
            return Ok();
        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        var user = await _userManager.FindByNameAsync(loginDTO.Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, loginDTO.Password))
        {
            var token = JwtHelper.GenerateToken(user, _configuration);
            return Ok(new { Token = token });
        }
        return Unauthorized();
    }
}