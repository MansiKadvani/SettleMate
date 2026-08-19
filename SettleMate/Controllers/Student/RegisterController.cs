using Microsoft.AspNetCore.Mvc;
using SettleMate.Data.Student;
using SettleMate.Models.Student;
using System.Text.Json;

namespace SettleMate.Controllers.Student
{
    [Route("Student/Register")]
    public class RegisterController : Controller
    {
        private readonly RegisterData registerData;

        // Create RegisterData object
        public RegisterController(IConfiguration configuration)
        {
            registerData = new RegisterData(configuration);
        }

        // Open registration page
        [HttpGet("")]
        public IActionResult Register()
        {
            try
            {
                return View("~/Views/Student/Register.cshtml");
            }
            catch (Exception)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        // Validate registration form and send OTP
        [HttpPost("SendOTP")]
        public IActionResult SendOTP(RegisterModel model)
        {
            try
            {
                // Check model validation
                if (!ModelState.IsValid)
                {
                    return View(
                        "~/Views/Student/Register.cshtml",
                        model);
                }

                // Check if email is already registered
                if (registerData.EmailExists(model.Email))
                {
                    ModelState.AddModelError(
                        "Email",
                        "Email already exists.");

                    return View(
                        "~/Views/Student/Register.cshtml",
                        model);
                }

                // Generate 6 digit OTP
                string otp = Random.Shared
                    .Next(100000, 1000000)
                    .ToString();

                // Store registration form data temporarily
                HttpContext.Session.SetString(
                    "PendingRegistration",
                    JsonSerializer.Serialize(model));

                // Store OTP temporarily
                HttpContext.Session.SetString(
                    "RegisterOTP",
                    otp);

                // Store OTP expiry time for 2 minutes
                HttpContext.Session.SetString(
                    "OTPExpiry",
                    DateTime.Now
                        .AddMinutes(2)
                        .ToString("O"));

                // Send OTP to registered email
                registerData.SendOTP(
                    model.Email,
                    otp);

                // Open OTP modal
                ViewBag.ShowOTP = true;
                ViewBag.OTPEmail = model.Email;

                return View(
                    "~/Views/Student/Register.cshtml",
                    model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(
                    "",
                    "Could not send OTP. Please try again.");

                return View(
                    "~/Views/Student/Register.cshtml",
                    model);
            }
        }

        // Verify OTP and complete registration
        [HttpPost("VerifyOTP")]
        public IActionResult VerifyOTP(string otp)
        {
            try
            {
                // Get saved OTP from session
                string savedOTP = HttpContext.Session.GetString(
                    "RegisterOTP");

                // Get saved expiry time from session
                string expiryText = HttpContext.Session.GetString(
                    "OTPExpiry");

                // Get saved registration data from session
                string registrationText = HttpContext.Session.GetString(
                    "PendingRegistration");

                // Check if session data exists
                if (savedOTP == null ||
                    expiryText == null ||
                    registrationText == null)
                {
                    TempData["Error"] =
                        "OTP session expired. Please register again.";

                    return Redirect("/Student/Register");
                }

                // Convert expiry text into DateTime
                DateTime expiry = DateTime.Parse(expiryText);

                // Convert saved registration text into model
                RegisterModel registerModel =
                    JsonSerializer.Deserialize<RegisterModel>(
                        registrationText);

                // Check if OTP has expired
                if (DateTime.Now > expiry)
                {
                    ModelState.AddModelError(
                        "",
                        "OTP has expired. Please resend OTP.");

                    ViewBag.ShowOTP = true;
                    ViewBag.OTPEmail = registerModel.Email;

                    return View(
                        "~/Views/Student/Register.cshtml",
                        registerModel);
                }

                // Check if OTP is empty or not 6 digits
                if (string.IsNullOrWhiteSpace(otp) ||
                    otp.Length != 6 ||
                    !otp.All(char.IsDigit))
                {
                    ModelState.AddModelError(
                        "",
                        "Please enter a valid 6 digit OTP.");

                    ViewBag.ShowOTP = true;
                    ViewBag.OTPEmail = registerModel.Email;

                    return View(
                        "~/Views/Student/Register.cshtml",
                        registerModel);
                }

                // Check if entered OTP is correct
                if (otp != savedOTP)
                {
                    ModelState.AddModelError(
                        "",
                        "Invalid OTP.");

                    ViewBag.ShowOTP = true;
                    ViewBag.OTPEmail = registerModel.Email;

                    return View(
                        "~/Views/Student/Register.cshtml",
                        registerModel);
                }

                // Check email again before database insert
                if (registerData.EmailExists(registerModel.Email))
                {
                    HttpContext.Session.Remove("RegisterOTP");
                    HttpContext.Session.Remove("OTPExpiry");
                    HttpContext.Session.Remove("PendingRegistration");

                    TempData["Error"] =
                        "Email already exists. Please register again.";

                    return Redirect("/Student/Register");
                }

                // Insert verified user into database
                bool result = registerData.Register(
                    registerModel);

                // Check if registration is successful
                if (result)
                {
                    // Clear temporary session data
                    HttpContext.Session.Remove("RegisterOTP");
                    HttpContext.Session.Remove("OTPExpiry");
                    HttpContext.Session.Remove("PendingRegistration");

                    TempData["Success"] =
                        "Registration successful. Please login.";

                    return RedirectToAction(
                        "Login",
                        "Login");
                }

                ModelState.AddModelError(
                    "",
                    "Registration failed. Please try again.");

                ViewBag.ShowOTP = true;
                ViewBag.OTPEmail = registerModel.Email;

                return View(
                    "~/Views/Student/Register.cshtml",
                    registerModel);
            }
            catch (Exception)
            {
                ModelState.AddModelError(
                    "",
                    "Something went wrong. Please try again.");

                return View(
                    "~/Views/Student/Register.cshtml");
            }
        }

        // Generate new OTP and send it again
        [HttpPost("ResendOTP")]
        public IActionResult ResendOTP()
        {
            try
            {
                // Get saved registration data
                string registrationText = HttpContext.Session.GetString(
                    "PendingRegistration");

                // Check if registration session exists
                if (registrationText == null)
                {
                    TempData["Error"] =
                        "Session expired. Please register again.";

                    return Redirect("/Student/Register");
                }

                // Convert session text into model
                RegisterModel model =
                    JsonSerializer.Deserialize<RegisterModel>(
                        registrationText);

                // Generate new 6 digit OTP
                string otp = Random.Shared
                    .Next(100000, 1000000)
                    .ToString();

                // Replace old OTP with new OTP
                HttpContext.Session.SetString(
                    "RegisterOTP",
                    otp);

                // Reset OTP expiry to 2 minutes
                HttpContext.Session.SetString(
                    "OTPExpiry",
                    DateTime.Now
                        .AddMinutes(2)
                        .ToString("O"));

                // Send new OTP email
                registerData.SendOTP(
                    model.Email,
                    otp);

                // Keep OTP modal open
                ViewBag.ShowOTP = true;
                ViewBag.OTPEmail = model.Email;
                ViewBag.ResendMessage =
                    "New OTP sent successfully.";

                return View(
                    "~/Views/Student/Register.cshtml",
                    model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(
                    "",
                    "Could not resend OTP. Please try again.");

                return View(
                    "~/Views/Student/Register.cshtml");
            }
        }
    }
}