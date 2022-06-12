using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Domain;
using UserManagement.Domain.Core;
using UsserManagement.Infrastructure;

namespace UsserManagement.DataAccessLayer.Repositories
{
    /// <summary>
    /// User repository implementation
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly UserManagementContext context;

        public UserRepository(UserManagementContext context)
        {
            this.context = context;
        }

        public IUnitOfWork UnitOfWork => context;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="user"></param>
        public User Insert(User user)
        {
            return this.context.Add(user).Entity;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="user"></param>
        public void Delete(User user)
        {
            this.context.Remove(user);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="user"></param>
        public User Update(User user)
        {
            return this.context.Update(user).Entity;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> FindByIdAsync(long id)
        {
            return await this.context.Users
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
