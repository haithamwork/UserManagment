    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagment.Application.Features.Users.Queries.Responses;
using UserManagment.Domain.Entities;

namespace UserManagment.Application.Mapping.Users
{
    public partial class UserProfile
    {
        public void GetUserListMapping()
        {
            CreateMap<User, GetUserListResponse>();
        }
    }
}
