using ECommerce.Core.Entities;

namespace ECommerce.Core.Interfaces;

public interface IUserRepository
{
    /// <summary>
    /// User Creation method for register
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<ApplicationUser?> AddUser(ApplicationUser user);
    /// <summary>
    /// User authentication method for login
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task<ApplicationUser?> AuthenticateUser(string? email, string? password);
}