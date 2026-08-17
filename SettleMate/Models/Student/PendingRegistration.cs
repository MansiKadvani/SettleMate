namespace SettleMate.Models.Student
{
    public class PendingRegistration
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ProfilePhoto { get; set; }
        public string Password { get; set; }

        public string Otp { get; set; }
        public DateTime OtpExpiryUtc { get; set; }

        public RegisterModel ToRegisterModel()
        {
            return new RegisterModel
            {
                Name = Name,
                Email = Email,
                Phone = Phone,
                Gender = Gender,
                City = City,
                Address = Address,
                ProfilePhoto = ProfilePhoto,
                Password = Password,
                ConfirmPassword = Password,
                Role = "Student"
            };
        }

        public RegisterModel ToDisplayModel()
        {
            return new RegisterModel
            {
                Name = Name,
                Email = Email,
                Phone = Phone,
                Gender = Gender,
                City = City,
                Address = Address,
                ProfilePhoto = ProfilePhoto,
                Role = "Student"
            };
        }
    }
}