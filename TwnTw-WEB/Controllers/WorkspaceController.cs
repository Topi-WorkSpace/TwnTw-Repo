using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwnTw_WEB.Data;
using TwnTw_WEB.DTO_Models;
using TwnTw_WEB.Models;

namespace TwnTw_WEB.Controllers
{
    public class WorkspaceController : Controller
    {
        private readonly TwnTwDbContext _context;
        private readonly IMapper _mapper;
        public WorkspaceController(TwnTwDbContext twnTwDbContext, IMapper mapper)
        {
            _context = twnTwDbContext;
            _mapper = mapper;
        }

        //Trả view tạo workspace
        [HttpGet]
        public async Task<IActionResult> CreateWorkspace()
        {
            return View();
        }

        //Thực hiện tạo workspace
        [HttpPost]
        public async Task<IActionResult> CreateWorkspace(WorkspaceCreateModel workspaceCreateModel)
        {
            await _context.Workspaces.AddAsync(_mapper.Map<Workspace>(workspaceCreateModel));
            await _context.SaveChangesAsync();
            return RedirectToAction("ListWorkspace");
        }

        //Trả view xem thông tin workspace
        [HttpGet]
        public async Task<IActionResult> ListWorkspace()
        {
            IEnumerable<Workspace> workspaces = await _context.Workspaces.ToListAsync();
            return View(workspaces);
        }


        // Trả view cập nhật workspace
        [HttpGet]
        public async Task<IActionResult> UpdateWorkspace(Guid id)
        {
            Workspace workspace = await _context.Workspaces.FirstOrDefaultAsync(x => x.WSId == id);
            return View(workspace);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateWorkspace(Workspace workspace)
        {
            _context.Workspaces.Update(workspace);
            await _context.SaveChangesAsync();
            return View();
        }


        //Trả view delete workspace
        

        [HttpGet]
        public async Task<IActionResult> ConfirmDelete(Guid id)
        {
            Workspace workspace = await _context.Workspaces.FirstOrDefaultAsync(x => x.WSId == id);
            _context.Workspaces.Remove(workspace);
            await _context.SaveChangesAsync();
            return RedirectToAction("ListWorkspace");
        }
    }
}
