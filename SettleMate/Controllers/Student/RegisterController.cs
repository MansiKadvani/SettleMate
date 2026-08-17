using Microsoft.AspNetCore.Mvc;
using SettleMate.Data.Student;
using SettleMate.Models.Student;

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
        // POST REGISTER
        // =====================================================

        [HttpPost("")]
        public IActionResult Register(RegisterModel model)
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


                // Save student

                bool result =
                    registerData.Register(model);


                if (result)
                {
                    TempData["Success"] =
                        "Registration successful.";

                    return Redirect("/Student/Register");
                }


                ModelState.AddModelError(
                    "",
                    "Registration failed.");

                return View(
                    "~/Views/Student/Register.cshtml",
                    model);
            }
            catch (Exception ex)
            {
                // Keep this while developing
                ModelState.AddModelError(
                    "",
                    "Error: " + ex.Message);

                return View(
                    "~/Views/Student/Register.cshtml",
                    model);
            }
        }
    }
}