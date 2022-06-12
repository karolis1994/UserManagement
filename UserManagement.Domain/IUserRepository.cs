using System.Threading;
using System.Threading.Tasks;
using UserManagement.Domain.Core;

namespace UserManagement.Domain
{
    /// <summary>
    /// User repository
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Add user
        /// </summary>
        /// <param name="user"></param>
        User Insert(User user);

        /// <summary>
        /// Edit user
        /// </summary>
        /// <param name="user"></param>
        User Update(User user);

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="user"></param>
        void Delete(User user);

        /// <summary>
        /// Get user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<User> FindByIdAsync(long id, CancellationToken cancellationToken);

        /// <summary>
        /// Checks if user exists by such name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
    }
}
