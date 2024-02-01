using Microsoft.AspNetCore.Mvc;

namespace TraceCV.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
