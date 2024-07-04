using MediatR;


namespace UserManagment.Application.Features.Users.Commands.Models
{
    public class DeleteUserCommand : IRequest<string>
    {
        public int Id { get; set; }
        public DeleteUserCommand(int Id)
        {
            this.Id = Id;
        }
    }
}
