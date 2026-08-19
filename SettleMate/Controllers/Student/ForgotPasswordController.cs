using Microsoft.AspNetCore.Mvc;
using SettleMate.Data.Student;
using SettleMate.Models.Student;

namespace SettleMate.Controllers.Student
{
    [Route("Student/ForgotPassword")]
    public class ForgotPasswordController : Controller
    {
        private readonly ForgotPasswordData forgotPasswordData;

        // Create ForgotPasswordData object
        public ForgotPasswordController(
            IConfiguration configuration)
        {
            forgotPasswordData =
                new ForgotPasswordData(configuration);
        }

        // Open forgot password page
        [HttpGet("")]
        public IActionResult ForgotPassword()
        {
            try
            {
                return View(
                    "~/Views/Student/ForgotPassword.cshtml");
            }
            catch (Exception)
            {
                return View(
                    "~/Views/Shared/Error.cshtml");
            }
        }

        // Check email and send OTP
        [HttpPost("SendOTP")]
        public IActionResult SendOTP(
            ForgotPasswordModel model)
        {
            try
            {
                // Check model validation
                if (!ModelState.IsValid)
                {
                    return View(
                        "~/Views/Student/ForgotPassword.cshtml",
                        model);
                }

                // Check if email is registered
                if (!forgotPasswordData.EmailExists(
                    model.Email))
                {
                    ModelState.AddModelError(
                        "Email",
                        "Email is not registered.");

                    return View(
                        "~/Views/Student/ForgotPassword.cshtml",
                        model);
                }

                // Generate 6 digit OTP
                string otp = Random.Shared
                    .Next(100000, 1000000)
                    .ToString();

                // Store email in session
                HttpContext.Session.SetString(
                    "ForgotPasswordEmail",
                    model.Email);

                // Store OTP in session
                HttpContext.Session.SetString(
                    "ForgotPasswordOTP",
                    otp);

                // Store expiry time for 2 minutes
                HttpContext.Session.SetString(
                    "ForgotPasswordOTPExpiry",
                    DateTime.Now
                        .AddMinutes(2)
                        .ToString("O"));

                // Send OTP to email
                forgotPasswordData.SendOTP(
                    model.Email,
                    otp);

                return RedirectToAction("OTP");
            }
            catch (Exception)
            {
                ModelState.AddModelError(
                    "",
                    "Could not send OTP. Please try again.");

                return View(
                    "~/Views/Student/ForgotPassword.cshtml",
                    model);
            }
        }

        // Open OTP verification page
        [HttpGet("OTP")]
        public IActionResult OTP()
        {
            try
            {
                // Get email from session
                string email = HttpContext.Session.GetString(
                    "ForgotPasswordEmail");

                // Redirect if session does not exist
                if (email == null)
                {
                    return RedirectToAction(
                        "ForgotPassword");
                }

                ViewBag.Email = email;

                return View(
                    "~/Views/Student/ForgotPasswordOTP.cshtml");
            }
            catch (Exception)
            {
                return RedirectToAction(
                    "ForgotPassword");
            }
        }

        // Verify entered OTP
        [HttpPost("VerifyOTP")]
        public IActionResult VerifyOTP(
            ForgotPasswordOTPModel model)
        {
            try
            {
                // Check OTP input validation
                if (!ModelState.IsValid)
                {
                    ViewBag.Email =
                        HttpContext.Session.GetString(
                            "ForgotPasswordEmail");

                    return View(
                        "~/Views/Student/ForgotPasswordOTP.cshtml",
                        model);
                }

                // Get OTP data from session
                string savedOTP = HttpContext.Session.GetString(
                    "ForgotPasswordOTP");

                string expiryText = HttpContext.Session.GetString(
                    "ForgotPasswordOTPExpiry");

                string email = HttpContext.Session.GetString(
                    "ForgotPasswordEmail");

                // Check if session exists
                if (savedOTP == null ||
                    expiryText == null ||
                    email == null)
                {
                    TempData["Error"] =
                        "OTP session expired. Please try again.";

                    return RedirectToAction(
                        "ForgotPassword");
                }

                // Convert expiry text into DateTime
                DateTime expiry = DateTime.Parse(expiryText);

                // Check OTP expiry
                if (DateTime.Now > expiry)
                {
                    ModelState.AddModelError(
                        "OTP",
                        "OTP has expired. Please resend OTP.");

                    ViewBag.Email = email;

                    return View(
                        "~/Views/Student/ForgotPasswordOTP.cshtml",
                        model);
                }

                // Check OTP value
                if (model.OTP != savedOTP)
                {
                    ModelState.AddModelError(
                        "OTP",
                        "Invalid OTP.");

                    ViewBag.Email = email;

                    return View(
                        "~/Views/Student/ForgotPasswordOTP.cshtml",
                        model);
                }

                // Mark OTP as verified
                HttpContext.Session.SetString(
                    "ForgotPasswordOTPVerified",
                    "true");

                return RedirectToAction(
                    "ResetPassword");
            }
            catch (Exception)
            {
                ModelState.AddModelError(
                    "",
                    "Something went wrong. Please try again.");

                ViewBag.Email =
                    HttpContext.Session.GetString(
                        "ForgotPasswordEmail");

                return View(
                    "~/Views/Student/ForgotPasswordOTP.cshtml",
                    model);
            }
        }

        // Open reset password page
        [HttpGet("ResetPassword")]
        public IActionResult ResetPassword()
        {
            try
            {
                // Check if OTP was verified
                string verified =
                    HttpContext.Session.GetString(
                        "ForgotPasswordOTPVerified");

                if (verified != "true")
                {
                    return RedirectToAction(
                        "ForgotPassword");
                }

                return View(
                    "~/Views/Student/ResetPassword.cshtml");
            }
            catch (Exception)
            {
                return RedirectToAction(
                    "ForgotPassword");
            }
        }

        // Change the user's password
        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(
            ResetPasswordModel model)
        {
            try
            {
                // Check password validation
                if (!ModelState.IsValid)
                {
                    return View(
                        "~/Views/Student/ResetPassword.cshtml",
                        model);
                }

                // Get verified status and email
                string verified =
                    HttpContext.Session.GetString(
                        "ForgotPasswordOTPVerified");

                string email =
                    HttpContext.Session.GetString(
                        "ForgotPasswordEmail");

                // Check if OTP was verified
                if (verified != "true" || email == null)
                {
                    TempData["Error"] =
                        "Session expired. Please try again.";

                    return RedirectToAction(
                        "ForgotPassword");
                }

                // Update password in database
                bool result =
                    forgotPasswordData.ChangePassword(
                        email,
                        model.NewPassword);

                if (result)
                {
                    // Clear session data
                    HttpContext.Session.Remove(
                        "ForgotPasswordEmail");

                    HttpContext.Session.Remove(
                        "ForgotPasswordOTP");

                    HttpContext.Session.Remove(
                        "ForgotPasswordOTPExpiry");

                    HttpContext.Session.Remove(
                        "ForgotPasswordOTPVerified");

                    TempData["Success"] =
                        "Password changed successfully. Please login.";

                    return RedirectToAction(
                        "Login",
                        "Login");
                }

                ModelState.AddModelError(
                    "",
                    "Password could not be changed.");

                return View(
                    "~/Views/Student/ResetPassword.cshtml",
                    model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(
                    "",
                    "Something went wrong. Please try again.");

                return View(
                    "~/Views/Student/ResetPassword.cshtml",
                    model);
            }
        }

        // Send a new OTP
        [HttpPost("ResendOTP")]
        public IActionResult ResendOTP()
        {
            try
            {
                // Get email from session
                string email =
                    HttpContext.Session.GetString(
                        "ForgotPasswordEmail");

                // Redirect if session does not exist
                if (email == null)
                {
                    TempData["Error"] =
                        "Session expired. Please try again.";

                    return RedirectToAction(
                        "ForgotPassword");
                }

                // Generate new 6 digit OTP
                string otp = Random.Shared
                    .Next(100000, 1000000)
                    .ToString();

                // Save new OTP in session
                HttpContext.Session.SetString(
                    "ForgotPasswordOTP",
                    otp);

                // Reset expiry time for 2 minutes
                HttpContext.Session.SetString(
                    "ForgotPasswordOTPExpiry",
                    DateTime.Now
                        .AddMinutes(2)
                        .ToString("O"));

                // Send new OTP to email
                forgotPasswordData.SendOTP(
                    email,
                    otp);

                TempData["Success"] =
                    "New OTP sent successfully.";

                return RedirectToAction("OTP");
            }
            catch (Exception)
            {
                TempData["Error"] =
                    "Could not resend OTP. Please try again.";

                return RedirectToAction("OTP");
            }
        }
    }
}