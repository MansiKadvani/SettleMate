using System.ComponentModel.DataAnnotations;

namespace SettleMate.Models.Student
{
    public class ForgotPasswordModel
    {
        // Store email entered on forgot password page
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; }
    }
}