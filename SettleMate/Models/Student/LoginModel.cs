using System.ComponentModel.DataAnnotations;

namespace SettleMate.Models.Student
{
    public class LoginModel
    {
        // Store email entered on login page
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; }

        // Store password entered on login page
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}