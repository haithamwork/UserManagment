using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagment.Application.Mapping.Users
{
    public partial class UserProfile : Profile
    {
        public UserProfile()
        {
            GetUserListMapping();
            AddUserCommandMapping();
            EditUserCommandMapping();
            GetUserByIdMapping();
            LoginUserCommandMapping();
        }

    }
}
