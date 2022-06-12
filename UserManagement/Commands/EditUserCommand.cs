using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.API.Models.Core;
using UserManagement.Domain;

namespace UserManagement.API.Commands
{
    /// <summary>
    /// Edits user's editable data
    /// </summary>
    public class EditUserCommand : IRequest<CommandResponse>
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [StringLength(120)]
        public string Email { get; set; }
    }

    internal sealed class EditUserCommandHandler : IRequestHandler<EditUserCommand, CommandResponse>
    {
        private readonly IUserRepository repository;

        public EditUserCommandHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CommandResponse> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var user = await repository.FindByIdAsync(request.Id, cancellationToken);

            if (user == null)
                return new CommandResponse(new AppError(nameof(UserManagementMessages.UserNotFound), UserManagementMessages.UserNotFound));

            user.UpdateData(request.Email);

            repository.Update(user);

            await repository.UnitOfWork.SaveChangesAsync();

            return new CommandResponse(user.Id);
        }
    }
}
