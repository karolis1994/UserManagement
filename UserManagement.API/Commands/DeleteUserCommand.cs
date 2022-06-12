using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.API.Models.Core;
using UserManagement.Domain;

namespace UserManagement.API.Commands
{
    /// <summary>
    /// Deletes user
    /// </summary>
    public class DeleteUserCommand : IRequest<CommandResponse>
    {
        public DeleteUserCommand(long id)
        {
            Id = id;
        }

        /// <summary>
        /// Identifier
        /// </summary>
        public long Id { get; }
    }

    internal sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, CommandResponse>
    {
        private readonly IUserRepository repository;

        public DeleteUserCommandHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CommandResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await repository.FindByIdAsync(request.Id, cancellationToken);

            if (user == null)
                return new CommandResponse(new AppError(nameof(UserManagementMessages.UserNotFound), UserManagementMessages.UserNotFound));

            repository.Delete(user);

            await repository.UnitOfWork.SaveChangesAsync();

            return CommandResponse.Success;
        }
    }
}
