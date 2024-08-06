using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TwnTw_WEB.Data;
using TwnTw_WEB.DTO_Models;
using TwnTw_WEB.Models;

namespace TwnTw_WEB.Controllers
{
    public class CalendarController : Controller
    {
        private readonly TwnTwDbContext _context;

        public CalendarController(TwnTwDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Process() { return View(); }

        public IActionResult Chart() { return View(); }

        public IActionResult WorkList() { return View(); }


        //Controller này trả view những công việc đã hoàn thành của từng user theo từng workspace
        public IActionResult DoneList() 
        {
            //Lấy userId từ session
            Guid userId = Guid.Parse(HttpContext.Session.GetString("UserId"));

            //Lấy danh sách memberDetail và workspace bằng userId
            List<MemberDetail> memberDetails = _context.MemberDetails.Where(x => x.UserId == userId).Include(a => a.Workspaces).Include(a => a.Users).ToList();

            //Lấy task đã hoàn thành theo userId
            List<TaskDetail> taskDetails = _context.TaskDetails.Where(x => x.Status == "Done" && x.UserId == userId).ToList();

            //Tạo list doneListByUserId
            List<TaskDetailViewModel_ForDoneList> doneListByUserId = new List<TaskDetailViewModel_ForDoneList>();
            //Thêm thông tin vào list doneListByUserId
            foreach (var item in memberDetails)
            {
                TaskDetailViewModel_ForDoneList taskDetailViewModel_ForDoneList = new TaskDetailViewModel_ForDoneList
                {
                    workSpace = item.Workspaces,
                    memberDetail = item,
                    taskDetails = taskDetails.Where(a => a.UserId == item.UserId).ToList()
                };
                //Add vào list doneListByUserId
                doneListByUserId.Add(taskDetailViewModel_ForDoneList);
            }

            return View(doneListByUserId);
        }
    }
}
