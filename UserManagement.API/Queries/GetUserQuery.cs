using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.API.Models;
using UserManagement.Domain;

namespace UserManagement.API.Queries
{
    /// <summary>
    /// Retrieves user
    /// </summary>
    public class GetUserQuery : IRequest<UserView>
    {
        public GetUserQuery(long id)
        {
            Id = id;
        }

        /// <summary>
        /// Id by which to find the user
        /// </summary>
        public long Id { get; }
    }

    internal sealed class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserView>
    {
        private readonly IUserRepository repository;

        public GetUserQueryHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<UserView> Handle(GetUserQuery query, CancellationToken cancellationToken)
        {
            var user = await repository.FindByIdAsync(query.Id, cancellationToken);

            if (user == null)
                return null;

            return new UserView()
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };
        }
    }
}
