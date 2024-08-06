using Microsoft.AspNetCore.Mvc;

namespace TwnTw_WEB.Controllers
{
    public class CalendarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Process() { return View(); }

        public IActionResult Chart() { return View(); }

        public IActionResult WorkList() { return View(); }

        public IActionResult DoneList() { return View(); }
    }
}
