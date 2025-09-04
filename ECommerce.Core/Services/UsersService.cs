using AutoMapper;
using ECommerce.Core.DTO;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;

namespace ECommerce.Core.Services;

internal class UsersService: IUsersService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public UsersService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<AuthenticationResponse?> Login(LoginRequestDTO loginRequest)
    {
        var user = await _userRepository.AuthenticateUser(loginRequest.Email, loginRequest.Password);

        if (user == null)
        {
            throw new UnauthorizedAccessException();
        }
        var userMapped = _mapper.Map<AuthenticationResponse>(user) with { Success = true, Token = "token"};
        return userMapped;
    }

    public async Task<AuthenticationResponse?> Register(RegisterRequestDTO registerRequest)
    {
        var newUser = _mapper.Map<ApplicationUser>(registerRequest);
        var user = await _userRepository.AddUser(newUser);
        if (user == null)
            throw new UnauthorizedAccessException();
        var userMapped = _mapper.Map<AuthenticationResponse>(user) with { Success = true, Token = "token" };
        return userMapped;
    }
}