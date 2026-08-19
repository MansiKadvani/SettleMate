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


        public RegisterController(IConfiguration configuration)
        {
            registerData =
                new RegisterData(configuration);
        }


        // =====================================================
        // GET REGISTER
        // =====================================================

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


        // =====================================================
        // SEND OTP
        // =====================================================

        [HttpPost("SendOTP")]
        public IActionResult SendOTP(RegisterModel model)
        {
            try
            {
                // Backend validation

                if (!ModelState.IsValid)
                {
                    return View(
                        "~/Views/Student/Register.cshtml",
                        model);
                }


                // Check email

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

                Random random = new Random();

                string otp =
                    random.Next(100000, 999999)
                    .ToString();


                // Store registration data

                HttpContext.Session.SetString(
                    "PendingRegistration",
                    JsonSerializer.Serialize(model));


                // Store OTP

                HttpContext.Session.SetString(
                    "RegisterOTP",
                    otp);


                // OTP expires after 2 minutes

                HttpContext.Session.SetString(
                    "OTPExpiry",
                    DateTime.Now
                        .AddMinutes(2)
                        .ToString());


                // Send OTP

                registerData.SendOTP(
                    model.Email,
                    otp);


                ViewBag.ShowOTP = true;

                ViewBag.OTPEmail =
                    model.Email;


                return View(
                    "~/Views/Student/Register.cshtml",
                    model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    "",
                    "Error: " + ex.Message);

                return View(
                    "~/Views/Student/Register.cshtml",
                    model);
            }
        }


        // =====================================================
        // VERIFY OTP
        // =====================================================

        [HttpPost("VerifyOTP")]
        public IActionResult VerifyOTP(string otp)
        {
            try
            {
                string savedOTP =
                    HttpContext.Session.GetString(
                        "RegisterOTP");


                string expiryText =
                    HttpContext.Session.GetString(
                        "OTPExpiry");


                string registrationText =
                    HttpContext.Session.GetString(
                        "PendingRegistration");


                if (savedOTP == null ||
                    expiryText == null ||
                    registrationText == null)
                {
                    TempData["Error"] =
                        "OTP session expired. Please register again.";

                    return Redirect("/Student/Register");
                }


                // Check expiry

                DateTime expiry =
                    DateTime.Parse(expiryText);


                if (DateTime.Now > expiry)
                {
                    ModelState.AddModelError(
                        "",
                        "OTP has expired.");

                    RegisterModel model =
                        JsonSerializer.Deserialize<RegisterModel>(
                            registrationText);

                    ViewBag.ShowOTP = true;

                    ViewBag.OTPEmail =
                        model.Email;

                    return View(
                        "~/Views/Student/Register.cshtml",
                        model);
                }


                // Check OTP

                if (otp != savedOTP)
                {
                    ModelState.AddModelError(
                        "",
                        "Invalid OTP.");

                    RegisterModel model =
                        JsonSerializer.Deserialize<RegisterModel>(
                            registrationText);

                    ViewBag.ShowOTP = true;

                    ViewBag.OTPEmail =
                        model.Email;

                    return View(
                        "~/Views/Student/Register.cshtml",
                        model);
                }


                // Get registration data

                RegisterModel registerModel =
                    JsonSerializer.Deserialize<RegisterModel>(
                        registrationText);


                // Finally insert into database

                bool result =
                    registerData.Register(
                        registerModel);


                if (result)
                {
                    // Clear session

                    HttpContext.Session.Remove(
                        "RegisterOTP");

                    HttpContext.Session.Remove(
                        "OTPExpiry");

                    HttpContext.Session.Remove(
                        "PendingRegistration");


                    TempData["Success"] =
        "Registration successful. Please login.";

                    return RedirectToAction(
                        "Login",
                        "Login");
                }


                ModelState.AddModelError(
                    "",
                    "Registration failed.");

                return View(
                    "~/Views/Student/Register.cshtml",
                    registerModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    "",
                    "Error: " + ex.Message);

                return View(
                    "~/Views/Student/Register.cshtml");
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
                string registrationText =
                    HttpContext.Session.GetString(
                        "PendingRegistration");


                if (registrationText == null)
                {
                    return Redirect(
                        "/Student/Register");
                }


                RegisterModel model =
                    JsonSerializer.Deserialize<RegisterModel>(
                        registrationText);


                Random random =
                    new Random();


                string otp =
                    random.Next(100000, 999999)
                    .ToString();


                HttpContext.Session.SetString(
                    "RegisterOTP",
                    otp);


                HttpContext.Session.SetString(
                    "OTPExpiry",
                    DateTime.Now
                        .AddMinutes(2)
                        .ToString());


                registerData.SendOTP(
                    model.Email,
                    otp);


                ViewBag.ShowOTP = true;

                ViewBag.OTPEmail =
                    model.Email;

                ViewBag.ResendMessage =
                    "New OTP sent successfully.";


                return View(
                    "~/Views/Student/Register.cshtml",
                    model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    "",
                    "Error: " + ex.Message);

                return View(
                    "~/Views/Student/Register.cshtml");
            }
        }
    }
}