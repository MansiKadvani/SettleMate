using System.Security.Cryptography;
using System.Text;

namespace SettleMate.Data
{
    public class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            try
            {
                using SHA256 sha256 = SHA256.Create();

                byte[] bytes = Encoding.UTF8.GetBytes(password);

                byte[] hash = sha256.ComputeHash(bytes);

                return Convert.ToBase64String(hash);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}