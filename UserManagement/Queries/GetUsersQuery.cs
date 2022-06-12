using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.API.Models;
using UsserManagement.Infrastructure;

namespace UserManagement.API.Queries
{
    /// <summary>
    /// Retrieves a list of all users
    /// </summary>
    public class GetUsersQuery : IRequest<IEnumerable<UserView>>
    {
    }

    internal sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserView>>
    {
        private readonly UserManagementContext context;

        public GetUsersQueryHandler(UserManagementContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<UserView>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
        {
            return await this.context.Users
                .Select(e => new UserView()
                {
                    Id = e.Id,
                    Email = e.Email,
                    Username = e.Username
                })
                .ToListAsync(cancellationToken);
        }
    }
}
