using System.Security.Cryptography;
using System.Text;

namespace SettleMate.Data
{
    public class PasswordHelper
    {
        // Convert password into hashed text
        public static string HashPassword(string password)
        {
            try
            {
                // Create SHA256 object
                using SHA256 sha256 =
                    SHA256.Create();

                // Convert password into bytes
                byte[] passwordBytes =
                    Encoding.UTF8.GetBytes(password);

                // Create hashed password bytes
                byte[] hashBytes =
                    sha256.ComputeHash(passwordBytes);

                // Convert hash bytes into text
                return Convert.ToBase64String(hashBytes);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}