using Microsoft.Data.SqlClient;
using SettleMate.Models.Student;
using System.Net;
using System.Net.Mail;

namespace SettleMate.Data.Student
{
    public class ForgotPasswordData
    {
        private readonly string connectionString;
        private readonly IConfiguration configuration;


        public ForgotPasswordData(IConfiguration configuration)
        {
            this.configuration = configuration;

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


                command.Parameters.AddWithValue(
                    "@Email",
                    email);


                connection.Open();


                int count =
                    Convert.ToInt32(
                        command.ExecuteScalar());


                return count > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }


        // =====================================================
        // SEND OTP
        // =====================================================

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
                    "SettleMate Password Reset OTP";


                message.Body =
                    "Hello,\n\n" +
                    "Your SettleMate password reset OTP is: " +
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
        // CHANGE PASSWORD
        // =====================================================

        public bool ChangePassword(
            string email,
            string newPassword)
        {
            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);


                string query = @"
                    UPDATE Users
                    SET Password = @Password
                    WHERE Email = @Email";


                using SqlCommand command =
                    new SqlCommand(query, connection);


                command.Parameters.AddWithValue(
                    "@Password",
                    PasswordHelper.HashPassword(newPassword));


                command.Parameters.AddWithValue(
                    "@Email",
                    email);


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