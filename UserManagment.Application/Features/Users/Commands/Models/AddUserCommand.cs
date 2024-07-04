using MediatR;


namespace UserManagment.Application.Features.Users.Commands.Models
{
    public class AddUserCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
