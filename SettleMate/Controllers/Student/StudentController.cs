using Microsoft.AspNetCore.Mvc;

namespace SettleMate.Controllers.Student
{
    public class StudentController : Controller
    {
        [HttpGet("Register")]
        public IActionResult Register()
        {
            try
            {
                return View("~/Views/Student/Home.cshtml");
            }
            catch (Exception)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}