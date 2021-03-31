using System;
using System.Security.Cryptography;

namespace libraryVueApp.Security
{
    public static class PasswordHasher
    {
        /// <summary>
        /// Size of salt.
        /// </summary>
        private const int _saltSize = 16;

        /// <summary>
        /// Size of hash.
        /// </summary>
        private const int _hashSize = 20;

        private const int _defaultiterations = 10000;

        /// <summary>
        /// Creates a hash from a password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="iterations">Number of iterations.</param>
        /// <returns>The hash.</returns>
        public static string Hash(string password, int iterations)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[_saltSize]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = pbkdf2.GetBytes(_hashSize);

            var hashBytes = new byte[_saltSize + _hashSize];
            Array.Copy(salt, 0, hashBytes, 0, _saltSize);
            Array.Copy(hash, 0, hashBytes, _saltSize, _hashSize);

            var base64Hash = Convert.ToBase64String(hashBytes);

            return base64Hash;
        }

        /// <summary>
        /// Creates a hash from a password with 10000 iterations
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>The hash.</returns>
        public static string Hash(string password)
        {
            return Hash(password, _defaultiterations);
        }

        /// <summary>
        /// Verifies a password against a hash.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="hashedPassword">The hash.</param>
        /// <returns>Could be verified?</returns>
        public static bool Verify(string password, string hashedPassword)
        {
            // Get hash bytes
            var hashBytes = Convert.FromBase64String(hashedPassword);

            // Get salt
            var salt = new byte[_saltSize];
            Array.Copy(hashBytes, 0, salt, 0, _saltSize);

            // Create hash with given salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, _defaultiterations);
            byte[] hash = pbkdf2.GetBytes(_hashSize);

            // Get result
            for (var i = 0; i < _hashSize; i++)
            {
                if (hashBytes[i + _saltSize] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
