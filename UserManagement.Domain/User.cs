using System;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Domain
{
    /// <summary>
    /// User
    /// </summary>
    public class User : Entity
    {
        /// <summary>
        /// Username
        /// </summary>
        [StringLength(50)]
        public string Username { get; private set; }

        /// <summary>
        /// Password
        /// </summary>
        [StringLength(1000)]
        public string Password { get; private set; }

        /// <summary>
        /// Salt used for password
        /// </summary>
        public byte[] Salt { get; private set; } = new byte[0];

        /// <summary>
        /// Email
        /// </summary>
        [StringLength(120)]
        public string? Email { get; private set; }

        /// <summary>
        /// Time when user was created in UTC
        /// </summary>
        public DateTime CreationTime { get; } = DateTime.UtcNow;

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static User CreateNew(string username, string password, string? email)
        {
            var salt = SaltGenerator.Generate();
            var hashedPassword = PasswordHasher.Hash(password, salt);

            return new User()
            {
                Salt = salt,
                Password = hashedPassword,
                Username = username,
                Email = email
            };
        }

        /// <summary>
        /// Updates user information
        /// </summary>
        /// <param name="email"></param>
        public void UpdateData(string? email)
        {
            this.Email = email;
        }
    }
}
