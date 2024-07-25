using Microsoft.AspNetCore.Mvc;

namespace TwnTw_WEB.Controllers
{
    public class WorkspaceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
