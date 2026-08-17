using Microsoft.AspNetCore.Mvc;

namespace SettleMate.Controllers.Student
{
    public class StudentController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}