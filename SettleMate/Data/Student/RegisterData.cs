using Microsoft.Data.SqlClient;
using SettleMate.Models.Student;
using System.Net;
using System.Net.Mail;

namespace SettleMate.Data.Student
{
    public class RegisterData
    {
        private readonly string connectionString;
        private readonly IConfiguration configuration;

        public RegisterData(IConfiguration configuration)
        {
            this.configuration = configuration;

            connectionString =
                configuration.GetConnectionString("DefaultConnection");
        }

        public bool SendOTP(string email, string otp)
        {
            try
            {
                string senderEmail =
                    configuration["EmailSettings:Email"];

                string senderPassword =
                    configuration["EmailSettings:Password"];

                string smtpServer =
                    configuration["EmailSettings:SmtpServer"];

                int port =
                    Convert.ToInt32(
                        configuration["EmailSettings:Port"]);


                MailMessage message =
                    new MailMessage();

                message.From =
                    new MailAddress(senderEmail);

                message.To.Add(email);

                message.Subject =
                    "SettleMate Email Verification";


                message.Body =
                    "Hello,\n\n" +
                    "Your SettleMate verification OTP is: " +
                    otp +
                    "\n\n" +
                    "This OTP is valid for 2 minutes.\n\n" +
                    "Thank you,\n" +
                    "SettleMate";


                SmtpClient smtp =
                    new SmtpClient(smtpServer);

                smtp.Port = port;

                smtp.Credentials =
                    new NetworkCredential(
                        senderEmail,
                        senderPassword);

                smtp.EnableSsl = true;

                smtp.Send(message);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
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