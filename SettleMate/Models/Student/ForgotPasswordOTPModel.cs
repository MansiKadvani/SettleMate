using System.ComponentModel.DataAnnotations;

namespace SettleMate.Models.Student
{
    public class ForgotPasswordOTPModel
    {
        [Required(ErrorMessage = "OTP is required")]
        [StringLength(6, MinimumLength = 6,
            ErrorMessage = "OTP must contain 6 digits")]
        public string OTP { get; set; }
    }
}