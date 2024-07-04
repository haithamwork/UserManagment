using Microsoft.EntityFrameworkCore;
using UserManagment.Domain.Entities;

using UserManagment.Infrastructure.Interfaces;
using UserManagment.Service.Interfaces;

namespace UserManagment.Service.Services
{
    public class UserService : IUserService
    {

        #region Fields
        private readonly IUserRepository _userRepository;
        #endregion

        #region Constructors
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        #region Handle Functions
        public async Task<List<User>> GetUsersListAsync()
        {
            return await _userRepository.GetUsersListAsync();
        }
        public async Task<string> AddAsync(User user)
        {
            //Check Existance
            var userObj=await _userRepository.GetTableNoTracking().Where(x=>x.Name.Equals(user.Name)).FirstOrDefaultAsync();
            if(userObj != null) { return "Exist"; }
            //Add 
            await _userRepository.AddAsync(user);
            return "Success";
        }
        public async Task<string> EditAsync(User user)
        {
            await _userRepository.UpdateAsync(user);
            return "Success";
        }
        public async Task<string> DeleteAsync(User user)
        {
            var trans=_userRepository.BeginTransaction();
            try
            {
                await _userRepository.DeleteAsync(user);
                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception)
            {
                await trans.RollbackAsync();
                return "Failed";
                
            }
            
        }
        public IQueryable<User> GetUsersQuerable()
        {
            return _userRepository.GetTableNoTracking().AsQueryable();
        }
        public async Task<User> GetByIdAsync(int Id)
        {
            var user = await _userRepository.GetByIdAsync(Id);
            return user;
        }

        public async Task<bool> CheckIfUserExistAsync(User user)
        {
            var result= await _userRepository.CheckIfUserExistAsync(user);
            return result;
        }

        #endregion



    }
}
