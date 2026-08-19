using Microsoft.AspNetCore.Mvc;
using SettleMate.Data.Student;
using SettleMate.Models.Student;

namespace SettleMate.Controllers.Student
{
    [Route("Student/Login")]
    public class LoginController : Controller
    {
        private readonly LoginData loginData;

        // Create LoginData object
        public LoginController(IConfiguration configuration)
        {
            loginData = new LoginData(configuration);
        }

        // Open login page
        [HttpGet("")]
        public IActionResult Login()
        {
            try
            {
                // Check if user is already logged in
                if (HttpContext.Session.GetInt32("UserID") != null)
                {
                    return Redirect("/Student/Home");
                }

                return View("~/Views/Student/Login.cshtml");
            }
            catch (Exception)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        // Check login details
        [HttpPost("")]
        public IActionResult Login(LoginModel model)
        {
            try
            {
                // Check if user is already logged in
                if (HttpContext.Session.GetInt32("UserID") != null)
                {
                    return Redirect("/Student/Home");
                }

                // Check model validation
                if (!ModelState.IsValid)
                {
                    return View(
                        "~/Views/Student/Login.cshtml",
                        model);
                }

                // Check email and password in database
                int userID = loginData.Login(model);

                // Login successful
                if (userID > 0)
                {
                    // Store user ID in session
                    HttpContext.Session.SetInt32(
                        "UserID",
                        userID);

                    // Store email in session
                    HttpContext.Session.SetString(
                        "UserEmail",
                        model.Email);

                    TempData["Success"] =
                        "Login successful.";

                    return Redirect("/Student/Home");
                }

                // Login failed
                ModelState.AddModelError(
                    "",
                    "Invalid email or password.");

                return View(
                    "~/Views/Student/Login.cshtml",
                    model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(
                    "",
                    "Something went wrong. Please try again.");

                return View(
                    "~/Views/Student/Login.cshtml",
                    model);
            }
        }
    }
}