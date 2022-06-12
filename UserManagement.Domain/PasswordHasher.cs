using System;
using System.Security.Cryptography;

namespace UserManagement.Domain
{
    /// <summary>
    /// Password hasher
    /// </summary>
    public static class PasswordHasher
    {
        /// <summary>
        /// Count of hashing iterations
        /// </summary>
        private const int IterationCount = 10000;

        /// <summary>
        /// Size of hash.
        /// </summary>
        private const int HashSize = 20;

        /// <summary>
        /// Hashes a given string
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string Hash(string password, byte[] salt)
        {
            // Create hash
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, IterationCount);
            var hash = pbkdf2.GetBytes(HashSize);

            // Combine salt and hash
            var hashBytes = new byte[SaltGenerator.SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltGenerator.SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltGenerator.SaltSize, HashSize);

            var base64Hash = Convert.ToBase64String(hashBytes);

            return base64Hash;
        }

        /// <summary>
        /// Verifies password validity
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static bool Verify(string password, byte[] salt, string hash)
        {
            var hashedPassword = Hash(password, salt);

            return string.Equals(hashedPassword, hash);
        }
    }
}
