using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.API.Models.Core;
using UserManagement.Domain;

namespace UserManagement.API.Commands
{
    /// <summary>
    /// Creates a new user
    /// </summary>
    public class CreateUserCommand : IRequest<CommandResponse>
    {
        /// <summary>
        /// Username
        /// </summary>
        [StringLength(50)]
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [StringLength(120)]
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [StringLength(30)]
        [MinLength(8)]
        [Required]
        public string Password { get; set; }
    }

    internal sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CommandResponse>
    {
        private readonly IUserRepository repository;

        public CreateUserCommandHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await repository.ExistsByNameAsync(request.Username, cancellationToken))
                return new CommandResponse(new AppError(nameof(UserManagementMessages.UserAlreadyExists), UserManagementMessages.UserAlreadyExists));

            var user = User.CreateNew(request.Username, request.Password, request.Email);

            repository.Insert(user);

            await repository.UnitOfWork.SaveChangesAsync();

            return new CommandResponse(user.Id);
        }
    }
}
