using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.DataProtection;

namespace TimesheetManagement.Library
{
    public class EncryptionLibrary
    {
        private static readonly IDataProtectionProvider _provider = DataProtectionProvider.Create("TimesheetManagement");

        // Protect plaintext using Data Protection with a given purpose
        public static string EncryptText(string input, string purpose)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (string.IsNullOrWhiteSpace(purpose)) throw new ArgumentException("Purpose is required", nameof(purpose));

            var protector = _provider.CreateProtector(purpose);
            return protector.Protect(input);
        }

        // Unprotect ciphertext using Data Protection with the same purpose
        public static string DecryptText(string input, string purpose)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (string.IsNullOrWhiteSpace(purpose)) throw new ArgumentException("Purpose is required", nameof(purpose));

            var protector = _provider.CreateProtector(purpose);
            return protector.Unprotect(input);
        }

        public static class KeyGenerator
        {
            public static string GetUniqueKey(int maxSize = 15)
            {
                if (maxSize <= 0) throw new ArgumentOutOfRangeException(nameof(maxSize));

                byte[] data = new byte[maxSize];
                RandomNumberGenerator.Fill(data);

                // Return base64-url without padding to avoid bias and keep URL-safe
                return Convert.ToBase64String(data)
                    .TrimEnd('=')
                    .Replace('+', '-')
                    .Replace('/', '_');
            }
        }
    }
}
