using AutoMapper;
using Azure;
using MediatR;
using UserManagment.Application.Features.Users.Commands.Models;
using UserManagment.Application.Features.Users.Queries.Models;
using UserManagment.Application.Features.Users.Queries.Responses;
using UserManagment.Service.Interfaces;

namespace UserManagment.Application.Features.Users.Queries.Handlers
{
    public class UserQueryHandler : IRequestHandler<GetUserListQuery,List<GetUserListResponse>>
                                   , IRequestHandler<GetUserByIdQuery, EditUserCommand>
    {
        #region Fields
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        #endregion
        #region Constructors
        public UserQueryHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        #endregion
        #region Handle Functions
        public async Task<List<GetUserListResponse>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var UsersList = await _userService.GetUsersListAsync();
            var UserListMapper = _mapper.Map<List<GetUserListResponse>>(UsersList);
            return UserListMapper;
        }
        public async Task<EditUserCommand> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByIdAsync(request.Id);
            if (user == null)
                return new EditUserCommand();
            var result = _mapper.Map<EditUserCommand>(user);
            return result;
        }
        #endregion
    }
}
