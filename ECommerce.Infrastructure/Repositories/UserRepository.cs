using Dapper;
using ECommerce.Core.Entities;
using ECommerce.Core.Enums;
using ECommerce.Core.Interfaces;
using ECommerce.Infrastructure.DbContext;

namespace ECommerce.Infrastructure.Repositories;

internal class UserRepository: IUserRepository
{
    private readonly DapperDbContext _context;

    public UserRepository(DapperDbContext context)
    {
        _context = context;
    }
    public async Task<ApplicationUser?> AddUser(ApplicationUser user)
    {
        user.UserID = Guid.NewGuid();
        
        string query = "INSERT INTO public.\"Users\"(\"UserID\", \"Email\", \"PersonName\", \"Gender\", \"Password\") VALUES (@UserID, @Email, @PersonName, @Gender, @Password)";
        var rows = await _context.DbConnection.ExecuteAsync(query, user);
        if (rows == 0)
            throw new ApplicationException($"User couldn't be added");
        
        return user;
    }

    public async Task<ApplicationUser?> AuthenticateUser(string? email, string? password)
    {
        string query = "Select * from public.\"Users\" Where \"Email\" = @Email And \"Password\" = @Password VALUES(@Email, @Password)";
        var parameters = new {Email = email, Password = password};
        var user = await _context.DbConnection.QueryFirstOrDefaultAsync<ApplicationUser>(query, parameters);
        if (user == null)
            throw new Exception("User not found");
        
        return user;
    }
}