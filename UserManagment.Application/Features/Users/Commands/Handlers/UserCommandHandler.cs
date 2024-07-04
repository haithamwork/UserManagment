using AutoMapper;
using MediatR;
using UserManagment.Application.Features.Users.Commands.Models;
using UserManagment.Domain.Entities;
using UserManagment.Service.Interfaces;

namespace UserManagment.Application.Features.Users.Commands.Handlers
{
    public class UserCommandHandler :     IRequestHandler<AddUserCommand,string>
                                         , IRequestHandler<EditUserCommand,string>
                                         , IRequestHandler<DeleteUserCommand, string>
                                         , IRequestHandler<LoginUserCommand,bool>
    {
        #region Fields
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        #endregion
        #region Constructors
        public UserCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        #endregion
        #region Handle Functions
        public async Task<string> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //Mapping
            var userMapper = _mapper.Map<User>(request);
            //Add
            var result = await _userService.AddAsync(userMapper);
            //Return Response
            if (result == "Success") 
                return "Created Successfully";
            else 
                return "";
        }

        public async Task<string> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByIdAsync(request.Id);
            if (user == null) return "user Is Not Found";
            var userMapper = _mapper.Map<EditUserCommand, User>(request,user);
            var result = await _userService.EditAsync(userMapper);
            if (result == "Success") return "Edited Successfully";
            else return "";
        }

        public async Task<string> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByIdAsync(request.Id);
            if (user == null) return "user Is Not Found";
            var result = await _userService.DeleteAsync(user);
            if (result == "Success") return "Deleted Successfully";
            else return "";
        }
        public async Task<bool> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var userMapper = _mapper.Map<User>(request);
            var result = await _userService.CheckIfUserExistAsync(userMapper);
            return result;
        }
        #endregion
    }
}
