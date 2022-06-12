using System;

namespace UserManagement.Domain
{
    public class User : Entity
    {
        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// Salt used for password
        /// </summary>
        public byte[] Salt { get; private set; } = new byte[0];

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; private set; }

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
        public static User CreateNew(string username, string password)
        {
            var salt = SaltGenerator.Generate();
            var hashedPassword = PasswordHasher.Hash(password, salt);

            return new User()
            {
                Salt = salt,
                Password = hashedPassword,
                Username = username
            };
        }

        /// <summary>
        /// Updates user information
        /// </summary>
        /// <param name="email"></param>
        public void UpdateData(string email)
        {
            this.Email = email;
        }
    }
}
