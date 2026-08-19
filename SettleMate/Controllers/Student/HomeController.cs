using Microsoft.AspNetCore.Mvc;

namespace SettleMate.Controllers.Student
{
    [Route("Student/Home")]
    public class HomeController : Controller
    {
        // ==========================================
        // HOME
        // ==========================================

        [HttpGet("")]
        public IActionResult Home()
        {
            try
            {
                int? userID =
                    HttpContext.Session.GetInt32("UserID");

                if (userID == null)
                {
                    return Redirect("/Student/Login");
                }

                return View("~/Views/Student/Home.cshtml");
            }
            catch (Exception)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }


        // ==========================================
        // LOGOUT
        // ==========================================

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            try
            {
                // Clear login session
                HttpContext.Session.Clear();

                // Go back to login page
                return Redirect("/Student/Login");
            }
            catch (Exception)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}