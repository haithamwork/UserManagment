
using UserManagment.Application.Features.Users.Commands.Models;
using UserManagment.Application.Features.Users.Queries.Responses;
using UserManagment.Domain.Entities;

namespace UserManagment.Application.Mapping.Users
{
    public partial class UserProfile
    {
        public void GetUserByIdMapping()
        {
            CreateMap<User, EditUserCommand>();
        }
    }
}
