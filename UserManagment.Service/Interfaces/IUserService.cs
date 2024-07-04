using UserManagment.Domain.Entities;

namespace UserManagment.Service.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetUsersListAsync();
        Task<string> AddAsync(User user);
        Task<string> EditAsync(User user);
        Task<string> DeleteAsync(User user);
        IQueryable<User> GetUsersQuerable();
        Task<User> GetByIdAsync(int Id);
        Task<bool> CheckIfUserExistAsync(User user);


    }
}
