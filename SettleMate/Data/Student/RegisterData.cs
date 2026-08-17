using Microsoft.Data.SqlClient;
using SettleMate.Models.Student;

namespace SettleMate.Data.Student
{
    public class RegisterData
    {
        private readonly string connectionString;

        public RegisterData(IConfiguration configuration)
        {
            connectionString =
                configuration.GetConnectionString("DefaultConnection");
        }


        // =====================================================
        // CHECK EMAIL
        // =====================================================

        public bool EmailExists(string email)
        {
            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);

                string query =
                    "SELECT COUNT(*) FROM Users WHERE Email = @Email";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Email", email);

                connection.Open();

                int count =
                    Convert.ToInt32(command.ExecuteScalar());

                return count > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }


        // =====================================================
        // REGISTER STUDENT
        // =====================================================

        public bool Register(RegisterModel model)
        {
            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);


                string query = @"
                    INSERT INTO Users
                    (
                        FullName,
                        Email,
                        MobileNumber,
                        Gender,
                        City,
                        Address,
                        ProfileAvatar,
                        Password,
                        Role,
                        IsActive,
                        CreatedDate
                    )
                    VALUES
                    (
                        @FullName,
                        @Email,
                        @MobileNumber,
                        @Gender,
                        @City,
                        @Address,
                        @ProfileAvatar,
                        @Password,
                        @Role,
                        @IsActive,
                        @CreatedDate
                    )";


                using SqlCommand command =
                    new SqlCommand(query, connection);


                command.Parameters.AddWithValue(
                    "@FullName",
                    model.Name);


                command.Parameters.AddWithValue(
                    "@Email",
                    model.Email);


                command.Parameters.AddWithValue(
                    "@MobileNumber",
                    model.Phone);


                command.Parameters.AddWithValue(
                    "@Gender",
                    model.Gender);


                command.Parameters.AddWithValue(
                    "@City",
                    model.City);


                command.Parameters.AddWithValue(
                    "@Address",
                    model.Address);


                command.Parameters.AddWithValue(
                    "@ProfileAvatar",
                    (object?)model.ProfilePhoto ?? DBNull.Value);


                command.Parameters.AddWithValue(
                    "@Password",
                    PasswordHelper.HashPassword(model.Password));


                command.Parameters.AddWithValue(
                    "@Role",
                    "Student");


                command.Parameters.AddWithValue(
                    "@IsActive",
                    true);


                command.Parameters.AddWithValue(
                    "@CreatedDate",
                    DateTime.Now);


                connection.Open();


                int result =
                    command.ExecuteNonQuery();


                return result > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}