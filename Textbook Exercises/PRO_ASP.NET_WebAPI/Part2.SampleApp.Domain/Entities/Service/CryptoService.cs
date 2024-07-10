using System;
using System.Security.Cryptography;
using System.Text;

namespace PingYourPackage.Domain
{
    public class CryptoService : ICryptoService
    {
        public string EncryptPassword(string password, string salt)
        {
            if(string.IsNullOrEmpty(password)) throw new ArgumentNullException("password");
            if(string.IsNullOrEmpty(salt)) throw new ArgumentNullException("salt");

            using(var sha256 = SHA256.Create())
            {
                var saltedPassword = string.Format("{0}{1}", salt, password);
                byte[] saltedpasswordasbytes = Encoding.UTF8.GetBytes(saltedPassword);
                return Convert.ToBase64String(sha256.ComputeHash(saltedpasswordasbytes));
            }
        }

        public string GenerateSalt()
        {
            var data = new byte[0x10];
            using(var cryptoserviceprovider = new RNGCryptoServiceProvider())
            {
                cryptoserviceprovider.GetBytes(data);
                return Convert.ToBase64String(data);
            }
        }
    }
}