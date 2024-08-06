using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwnTw_WEB.Data;
using TwnTw_WEB.DTO_Models;
using TwnTw_WEB.Models;

namespace TwnTw_WEB.Controllers
{
    public class TaskDetailController : Controller
    {
        private readonly TwnTwDbContext _context;
        private readonly IMapper _mapper;
        public TaskDetailController(TwnTwDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public IActionResult Index()
        {
            ViewData["UserName"] = HttpContext.Session.GetString("UserName");
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ListTaskDetail()
        {
            var list = await _context.TaskDetails.ToListAsync();    
            return View(list);
        }
        public IActionResult CreateTaskDetail()
        {
            return View(); 
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateTaskDetail(TaskDetailCreateModel taskDetailCreateModel)
        {
            
            var taskDetail = _mapper.Map<TaskDetail>(taskDetailCreateModel);
            
            await _context.TaskDetails.AddAsync(taskDetail);
            await _context.SaveChangesAsync();

            return View();
        }
        public IActionResult UpdateTaskDetail()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTaskDetail(Guid Id)
        {
            var taskDetail = await _context.TaskDetails.FindAsync(Id);

            if(taskDetail == null)
            {
                return NotFound("Id khoong hop le");
            }
            if (await TryUpdateModelAsync(taskDetail, "",
                s => s.Description,
                s => s.CreatedDate,
                s => s.Status
                ))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
                return RedirectToAction("ListTaskDetail");
            }       
            return View(taskDetail);
        }
        public IActionResult DeleteTaskDetail()
        {
            return View(); 
        }
        [HttpGet]
        public async Task<IActionResult> DeleteTaskDetail(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskDetail = await _context.TaskDetails.FindAsync(id);

            if (taskDetail != null)
            {
                _context.TaskDetails.Remove(taskDetail);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("ListTaskDetail");
        }
    }
}
