using System.Security.Cryptography;

namespace UserManagement.Domain
{
    /// <summary>
    /// Password salt generator
    /// </summary>
    public static class SaltGenerator
    {
        private static RNGCryptoServiceProvider cryptoServiceProvider = new RNGCryptoServiceProvider();

        /// <summary>
        /// Default salt size
        /// </summary>
        public const int SaltSize = 32;

        /// <summary>
        /// Generates salt
        /// </summary>
        /// <param name="maximumSaltLength">maximum length of salt to be generated</param>
        /// <returns></returns>
        public static byte[] Generate()
        {
            var salt = new byte[SaltSize];
            cryptoServiceProvider.GetNonZeroBytes(salt);

            return salt;
        }
    }
}
