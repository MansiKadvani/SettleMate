using System.ComponentModel.DataAnnotations;

namespace SettleMate.Models.Student
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "New password is required")]
        [StringLength(50, MinimumLength = 8,
            ErrorMessage = "Password must be at least 8 characters")]
        public string NewPassword { get; set; }


        [Required(ErrorMessage = "Please confirm your password")]
        [Compare("NewPassword",
            ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}