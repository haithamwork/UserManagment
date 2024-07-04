using Azure;
using MediatR;
using UserManagment.Application.Features.Users.Commands.Models;
using UserManagment.Application.Features.Users.Queries.Responses;

namespace UserManagment.Application.Features.Users.Queries.Models
{
    public class GetUserByIdQuery : IRequest<EditUserCommand>
    {
        public int Id { get; set; }
        public GetUserByIdQuery(int Id)
        {
            this.Id = Id;
        }

    }
}
