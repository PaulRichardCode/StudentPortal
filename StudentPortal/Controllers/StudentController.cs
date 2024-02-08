using Microsoft.AspNetCore.Mvc;

namespace StudentPortal.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}
