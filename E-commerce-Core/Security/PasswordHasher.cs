using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.Security
{
    public class PasswordHasher
    {
        private const int SaltSize = 16;   // 128-bit
        private const int KeySize = 32;    // 256-bit
        private const int Iterations = 10000;

        private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA256;

        public static string Hash(string password)
        {
            // generate salt
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            // derive key using PBKDF2
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                HashAlgorithm,
                KeySize);

            // format: {iterations}.{salt base64}.{hash base64}
            return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        public static bool Verify(string password, string hashString)
        {
            // split into parts
            var parts = hashString.Split('.');
            if (parts.Length != 3)
                return false;

            int iterations = int.Parse(parts[0]);
            byte[] salt = Convert.FromBase64String(parts[1]);
            byte[] hash = Convert.FromBase64String(parts[2]);

            // derive new hash using same parameters
            byte[] hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                iterations,
                HashAlgorithm,
                hash.Length);

            // compare
            return CryptographicOperations.FixedTimeEquals(hashToCompare, hash);
        }
    }
}
