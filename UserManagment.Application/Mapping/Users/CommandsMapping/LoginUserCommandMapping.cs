using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagment.Application.Features.Users.Commands.Models;
using UserManagment.Domain.Entities;

namespace UserManagment.Application.Mapping.Users
{
    public partial class UserProfile
    {
        public void LoginUserCommandMapping()
        {

            CreateMap<LoginUserCommand, User>();
        }
    }
}
