using Microsoft.AspNetCore.Mvc;
using SettleMate.Data.Student;
using SettleMate.Models.Student;

namespace SettleMate.Controllers.Student
{
    [Route("Student/ForgotPassword")]
    public class ForgotPasswordController : Controller
    {
        private readonly ForgotPasswordData forgotPasswordData;


        public ForgotPasswordController(
            IConfiguration configuration)
        {
            forgotPasswordData =
                new ForgotPasswordData(configuration);
        }


        // =====================================================
        // PAGE 1 - ENTER EMAIL
        // =====================================================

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


        // =====================================================
        // SEND OTP
        // =====================================================

        [HttpPost("SendOTP")]
        public IActionResult SendOTP(
            ForgotPasswordModel model)
        {
            try
            {
                // Frontend + backend validation

                if (!ModelState.IsValid)
                {
                    return View(
                        "~/Views/Student/ForgotPassword.cshtml",
                        model);
                }


                // Check email

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


                // Generate OTP

                Random random =
                    new Random();


                string otp =
                    random.Next(100000, 999999)
                    .ToString();


                // Store email

                HttpContext.Session.SetString(
                    "ForgotPasswordEmail",
                    model.Email);


                // Store OTP

                HttpContext.Session.SetString(
                    "ForgotPasswordOTP",
                    otp);


                // Store expiry

                HttpContext.Session.SetString(
                    "ForgotPasswordOTPExpiry",
                    DateTime.Now
                        .AddMinutes(2)
                        .ToString());


                // Send email

                forgotPasswordData.SendOTP(
                    model.Email,
                    otp);


                return RedirectToAction(
                    "OTP");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    "",
                    "Error: " + ex.Message);

                return View(
                    "~/Views/Student/ForgotPassword.cshtml",
                    model);
            }
        }


        // =====================================================
        // PAGE 2 - OTP
        // =====================================================

        [HttpGet("OTP")]
        public IActionResult OTP()
        {
            try
            {
                string email =
                    HttpContext.Session.GetString(
                        "ForgotPasswordEmail");


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


        // =====================================================
        // VERIFY OTP
        // =====================================================

        [HttpPost("VerifyOTP")]
        public IActionResult VerifyOTP(
            ForgotPasswordOTPModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Email =
                        HttpContext.Session.GetString(
                            "ForgotPasswordEmail");

                    return View(
                        "~/Views/Student/ForgotPasswordOTP.cshtml",
                        model);
                }


                string savedOTP =
                    HttpContext.Session.GetString(
                        "ForgotPasswordOTP");


                string expiryText =
                    HttpContext.Session.GetString(
                        "ForgotPasswordOTPExpiry");


                string email =
                    HttpContext.Session.GetString(
                        "ForgotPasswordEmail");


                if (savedOTP == null ||
                    expiryText == null ||
                    email == null)
                {
                    TempData["Error"] =
                        "OTP expired. Please try again.";

                    return RedirectToAction(
                        "ForgotPassword");
                }


                DateTime expiry =
                    DateTime.Parse(expiryText);


                // Check expiry

                if (DateTime.Now > expiry)
                {
                    ModelState.AddModelError(
                        "OTP",
                        "OTP has expired.");


                    ViewBag.Email = email;


                    return View(
                        "~/Views/Student/ForgotPasswordOTP.cshtml",
                        model);
                }


                // Check OTP

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


                // OTP correct

                HttpContext.Session.SetString(
                    "ForgotPasswordOTPVerified",
                    "true");


                return RedirectToAction(
                    "ResetPassword");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    "",
                    "Error: " + ex.Message);


                ViewBag.Email =
                    HttpContext.Session.GetString(
                        "ForgotPasswordEmail");


                return View(
                    "~/Views/Student/ForgotPasswordOTP.cshtml",
                    model);
            }
        }


        // =====================================================
        // PAGE 3 - RESET PASSWORD
        // =====================================================

        [HttpGet("ResetPassword")]
        public IActionResult ResetPassword()
        {
            try
            {
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


        // =====================================================
        // CHANGE PASSWORD
        // =====================================================

        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(
            ResetPasswordModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(
                        "~/Views/Student/ResetPassword.cshtml",
                        model);
                }


                string verified =
                    HttpContext.Session.GetString(
                        "ForgotPasswordOTPVerified");


                string email =
                    HttpContext.Session.GetString(
                        "ForgotPasswordEmail");


                if (verified != "true" ||
                    email == null)
                {
                    TempData["Error"] =
                        "Session expired. Please try again.";


                    return RedirectToAction(
                        "ForgotPassword");
                }


                // Change password

                bool result =
                    forgotPasswordData.ChangePassword(
                        email,
                        model.NewPassword);


                if (result)
                {
                    // Clear forgot password session

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
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    "",
                    "Error: " + ex.Message);


                return View(
                    "~/Views/Student/ResetPassword.cshtml",
                    model);
            }
        }


        // =====================================================
        // RESEND OTP
        // =====================================================

        [HttpPost("ResendOTP")]
        public IActionResult ResendOTP()
        {
            try
            {
                string email =
                    HttpContext.Session.GetString(
                        "ForgotPasswordEmail");


                if (email == null)
                {
                    return RedirectToAction(
                        "ForgotPassword");
                }


                Random random =
                    new Random();


                string otp =
                    random.Next(100000, 999999)
                    .ToString();


                HttpContext.Session.SetString(
                    "ForgotPasswordOTP",
                    otp);


                HttpContext.Session.SetString(
                    "ForgotPasswordOTPExpiry",
                    DateTime.Now
                        .AddMinutes(2)
                        .ToString());


                forgotPasswordData.SendOTP(
                    email,
                    otp);


                TempData["Success"] =
                    "New OTP sent successfully.";


                return RedirectToAction(
                    "OTP");
            }
            catch (Exception)
            {
                return RedirectToAction(
                    "OTP");
            }
        }
    }
}