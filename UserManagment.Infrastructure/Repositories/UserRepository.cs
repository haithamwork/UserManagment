using Microsoft.EntityFrameworkCore;
using UserManagment.Domain.Entities;
using UserManagment.Infrastructure.DataBase;
using UserManagment.Infrastructure.Generic;
using UserManagment.Infrastructure.Interfaces;


namespace UserManagment.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        #region Fields
        private readonly DbSet<User> _user;
        #endregion

        #region Constructors
        public UserRepository(ApplicationDbContext context):base(context) 
        {
            _user = context.Set<User>();
        }

        

        #endregion

        #region Handle Functions
        public async Task<List<User>> GetUsersListAsync()
        {
            return await _user.ToListAsync();
        }
        public async Task<bool> CheckIfUserExistAsync(User user)
        {
            var result= await _user.FirstOrDefaultAsync(c=>c.Email==user.Email&&c.Password==user.Password);
            if(result==null)
            {
                return false;
            }
            return true;
        }

        #endregion




    }
}
