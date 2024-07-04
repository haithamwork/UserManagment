using MediatR;


namespace UserManagment.Application.Features.Users.Commands.Models
{
    public class LoginUserCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
