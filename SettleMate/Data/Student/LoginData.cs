using Microsoft.Data.SqlClient;
using SettleMate.Models.Student;

namespace SettleMate.Data.Student
{
    public class LoginData
    {
        private readonly string connectionString;

        public LoginData(IConfiguration configuration)
        {
            connectionString =
                configuration.GetConnectionString("DefaultConnection");
        }

        public int Login(LoginModel model)
        {
            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);

                string query = @"
            SELECT UserID
            FROM Users
            WHERE Email = @Email
            AND Password = @Password
            AND Role = 'Student'";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                command.Parameters.AddWithValue(
                    "@Email",
                    model.Email);

                command.Parameters.AddWithValue(
                    "@Password",
                    PasswordHelper.HashPassword(model.Password));

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    return Convert.ToInt32(result);
                }

                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}