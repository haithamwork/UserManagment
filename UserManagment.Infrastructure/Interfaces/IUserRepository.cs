using UserManagment.Domain.Entities;
using UserManagment.Infrastructure.Generic;

namespace UserManagment.Infrastructure.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<List<User>> GetUsersListAsync();
        Task<bool> CheckIfUserExistAsync(User user);
    }
}
