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
        Task<User> FindByIdAsync(long id);
    }
}
