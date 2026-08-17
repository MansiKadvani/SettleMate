using System.ComponentModel.DataAnnotations;

namespace SettleMate.Models.Student
{
    public class RegisterModel
    {
        public int UserID { get; set; }


        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "Name must be between 3 and 100 characters")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Mobile number is required")]
        [RegularExpression(@"^[0-9]{10}$",
            ErrorMessage = "Mobile number must contain 10 digits")]
        public string Phone { get; set; }


        [Required(ErrorMessage = "Please select gender")]
        public string Gender { get; set; }


        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }


        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }


        public string? ProfilePhoto { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, MinimumLength = 8,
            ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Please confirm password")]
        [Compare("Password",
            ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }


        public string Role { get; set; } = "Student";
    }
}