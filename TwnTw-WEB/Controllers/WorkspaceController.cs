using Microsoft.AspNetCore.Mvc;

namespace TwnTw_WEB.Controllers
{
    public class WorkspaceController : Controller
    {
        //Trả view tạo workspace
        public async Task<IActionResult> CreateWorkspace()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateWorkspace(MOd)
        //{//Xử lý tạo workspace


        //    return View();
        //}
    }
}
