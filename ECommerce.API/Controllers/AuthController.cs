using AutoMapper;
using ECommerce.Core.DTO;
using ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUsersService _usersService;
    private readonly IMapper mapper;
    public AuthController(IUsersService usersService, IMapper _mapper)
    {
        _usersService = usersService;
        mapper = _mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDTO registerRequest)
    {
        if(registerRequest == null)
            throw new ArgumentNullException(nameof(registerRequest));
        
        var user = await _usersService.Register(registerRequest);
        
        if(user == null || user.Success == false)
            throw new Exception("User cannot be created");
        
        return Ok(user);
        
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDTO loginRequest)
    {
        if(loginRequest == null)
            throw new ArgumentNullException(nameof(loginRequest));
        
        var user = await _usersService.Login(loginRequest);

        if (user == null || user.Success == false)
        {
            throw new Exception("User can't be found");
        }
        
        return Ok(user);
    }
}