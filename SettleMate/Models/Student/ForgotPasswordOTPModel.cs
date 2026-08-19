using System.ComponentModel.DataAnnotations;

namespace SettleMate.Models.Student
{
    public class ForgotPasswordOTPModel
    {
        // Store OTP entered by user
        [Required(ErrorMessage = "OTP is required")]
        [RegularExpression(@"^\d{6}$",
            ErrorMessage = "OTP must contain 6 digits")]
        public string OTP { get; set; }
    }
}