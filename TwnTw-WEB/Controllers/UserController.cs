using Microsoft.AspNetCore.Mvc;

namespace TwnTw_WEB.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
