using ECommerce.Core.DTO;

namespace ECommerce.Core.Interfaces;

public interface IUsersService
{
    /// <summary>
    /// Login Method
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <returns></returns>
    Task<AuthenticationResponse?> Login(LoginRequestDTO loginRequest);
    /// <summary>
    /// Register Method
    /// </summary>
    /// <param name="registerRequest"></param>
    /// <returns></returns>
    Task<AuthenticationResponse?> Register(RegisterRequestDTO registerRequest);
}