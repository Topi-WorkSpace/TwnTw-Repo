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

        public IActionResult Process() 
        {
            //Lấy userId từ session
            Guid userId = Guid.Parse(HttpContext.Session.GetString("UserId"));

            //Lấy danh sách memberDetail và workspace bằng userId
            List<MemberDetail> memberDetails = _context.MemberDetails.Where(x => x.UserId == userId).Include(a => a.Workspaces).Include(a => a.Users).ToList();

            //Lấy danh sách task theo userId
            List<TaskDetail> taskDetails = _context.TaskDetails.Where(x => x.UserId == userId).ToList();

            //Tạo list taskListByUserId
            List<TaskDetailViewModel_ForList> taskListByUserId = new List<TaskDetailViewModel_ForList>();
            foreach (var item in memberDetails)
            {
                TaskDetailViewModel_ForList taskDetailViewModel_ForList = new TaskDetailViewModel_ForList
                {
                    workSpace = item.Workspaces,
                    memberDetail = item,
                    taskDetails = taskDetails
                };
                taskListByUserId.Add(taskDetailViewModel_ForList);
            }

            return View(taskListByUserId); 
        }

        public IActionResult Chart() { return View(); }

        //Controller này trả về view những công việc đang được thực hiện của từng user theo từng workspace
        public IActionResult WorkList() 
        {
            //Lấy userId từ session
            Guid userId = Guid.Parse(HttpContext.Session.GetString("UserId"));

            //Lấy danh sách memberDetail và workspace bằng userId
            List<MemberDetail> memberDetails = _context.MemberDetails.Where(x => x.UserId == userId).Include(a => a.Workspaces).Include(a => a.Users).ToList();

            //Lấy task trạng thái processing theo userId
            List<TaskDetail> taskDetails = _context.TaskDetails.Where(x => x.Status == "Processing" && x.UserId == userId).ToList();

            //Tạo list workListByUserId
            List<TaskDetailViewModel_ForList> workListByUserId = new List<TaskDetailViewModel_ForList>();

            //Thêm thông tin vào list workListByUserId
            foreach (var item in memberDetails)
            {
                TaskDetailViewModel_ForList taskDetailViewModel_ForList = new TaskDetailViewModel_ForList
                {
                    workSpace = item.Workspaces,
                    memberDetail = item,
                    taskDetails = taskDetails
                };
                //Add vào list workListByUserId
                workListByUserId.Add(taskDetailViewModel_ForList);
            }

            return View(workListByUserId); 
        }


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
            List<TaskDetailViewModel_ForList> doneListByUserId = new List<TaskDetailViewModel_ForList>();
            //Thêm thông tin vào list doneListByUserId
            foreach (var item in memberDetails)
            {
                TaskDetailViewModel_ForList taskDetailViewModel_ForDoneList = new TaskDetailViewModel_ForList
                {
                    workSpace = item.Workspaces,
                    memberDetail = item,
                    taskDetails = taskDetails
                };
                //Add vào list doneListByUserId
                doneListByUserId.Add(taskDetailViewModel_ForDoneList);
            }

            return View(doneListByUserId);
        }
    }
}
