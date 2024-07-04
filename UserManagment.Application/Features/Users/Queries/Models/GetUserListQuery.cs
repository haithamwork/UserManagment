using MediatR;

using UserManagment.Application.Features.Users.Queries.Responses;

namespace UserManagment.Application.Features.Users.Queries.Models
{
    public class GetUserListQuery : IRequest<List<GetUserListResponse>>
    {
    }
}
