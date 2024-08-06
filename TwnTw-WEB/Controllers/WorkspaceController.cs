using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using TwnTw_WEB.Data;
using TwnTw_WEB.DTO_Models;
using TwnTw_WEB.Models;
using TwnTw_WEB.Models.ViewModel;

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
            //tạo workspace
            var wsp = _mapper.Map<Workspace>(workspaceCreateModel);
            await _context.Workspaces.AddAsync(_mapper.Map<Workspace>(wsp));
            await _context.SaveChangesAsync();

            //Tạo memberDetail cho người tạo workspace (role leader)
            MemberDetail memberDetail = new MemberDetail
            {
                MemberDetailId = Guid.NewGuid(),
                WorkSpaceId = wsp.WSId,
                Role = "Leader",
                Status = "Accepted",
                UserId = Guid.Parse(HttpContext.Session.GetString("UserId"))
            };
            await _context.MemberDetails.AddAsync(memberDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction("ListWorkspace");
        }

        //Trả view xem thông tin workspace
        [HttpGet]
        public async Task<IActionResult> ListWorkspace(string userId)
        {
            userId = HttpContext.Session.GetString("UserId");
            if (userId != string.Empty)
            {
                IEnumerable<Workspace> workspaces = await (from md in _context.MemberDetails
                                                           join ws in _context.Workspaces
                                                           on md.WorkSpaceId equals ws.WSId
                                                           where md.UserId == Guid.Parse(userId)
                                                           select ws)
                                                           .ToListAsync();
                return View(workspaces);
            }
            return View(new List<Workspace>());
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

        [HttpGet]
        public async Task<IActionResult> Join()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Join(string wSId)
        {
            if (string.IsNullOrEmpty(wSId) || string.IsNullOrWhiteSpace(wSId))
            {
                TempData["Error"] = "Hãy nhập mã của work space";
                return View();
            }
            var userId = HttpContext.Session.GetString("UserId");

            MemberDetail memberDetail = new MemberDetail
            {
                MemberDetailId = Guid.NewGuid(),
                WorkSpaceId = Guid.Parse(wSId),
                Role = "Member",
                Status = "Accepted",
                UserId = Guid.Parse(HttpContext.Session.GetString("UserId"))
            };

            /*var eMember = await _context.MemberDetails.FirstOrDefaultAsync(memberDetail.);*/

            var join = await _context.MemberDetails.AddAsync(memberDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction("ListWorkspace","Workspace");
        }

        public async Task<IActionResult> IntoWS(Guid id)
        {
                var members = await (from user in _context.Users
                               join member in _context.MemberDetails
                               on user.UserId equals member.UserId
                               join workspace in _context.Workspaces
                               on member.WorkSpaceId equals workspace.WSId
                               where member.WorkSpaceId == id
                               select new TwnTw_WEB.Models.ViewModel.MemberDetailViewModel
                               {
                                   UserName = user.UserName,
                                   Email = user.Email,
                                   UserId = user.UserId,
                                   WSId = id,
                                   WSName = workspace.WSName,
                                   Role = member.Role,
                                   Status = member.Status,
                               }).ToListAsync();

            var listTask = await (from task in _context.TaskDetails
                            join user in _context.Users
                            on task.UserId equals user.UserId
                            join memberD in _context.MemberDetails
                            on user.UserId equals memberD.UserId
                            where memberD.WorkSpaceId == id
                            select new TaskDetailListViewModel
                            {
                                TaskDetailId = task.TaskDetailId,
                                UserId = user.UserId,
                                UserName = user.UserName,
                                Description = task.Description,
                                Status = task.Status,
                                CreatedDate = task.CreatedDate,
                                EndDate = task.EndDate,
                            }).ToListAsync();

            ViewBag.ListTask = listTask;



            return View(members);
        }

        public async Task<IActionResult> Assign(string userId, string WSId)
        {
            var assineeId = HttpContext.Session.GetString("UserId");

            var user = await _context.MemberDetails.FirstOrDefaultAsync(m => m.UserId == Guid.Parse(assineeId) && m.WorkSpaceId == Guid.Parse(WSId));

            if (user.Role != "Leader")
            {
                TempData["Notify"] = "Bạn không có quyên này";
                return RedirectToAction("IntoWS", new { id = user.WorkSpaceId });
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Assign(TaskDetail task, string WSId, string userId)
        {
            task.TaskDetailId = Guid.NewGuid();
            task.UserId = Guid.Parse(userId);
            task.CreatedDate = DateTime.Now;
            await _context.AddAsync(task);
            await _context.SaveChangesAsync();

            return RedirectToAction("IntoWS", new { id = WSId });
        }



        public IActionResult AddMember(string WSId)
        {
            AddMemberDetailDto member = new AddMemberDetailDto()
            {
                WSId = WSId
            };

            var userId = HttpContext.Session.GetString("UserId");
            
            var findUser = _context.MemberDetails.FirstOrDefault(m => m.UserId == Guid.Parse(userId));

            if (findUser.Role != "Leader")
            {
                TempData["Notify"] = "Bạn không có quyền này";
                return RedirectToAction("IntoWS", new { id = WSId });
            }

            return View(member);
        }

        [HttpPost]
        public async Task<IActionResult> AddMember(AddMemberDetailDto model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user == null)
            {
                TempData["Notify"] = "Không có người dùng này";
                return RedirectToAction("AddMember", new { WSId = model.WSId });
            }

            MemberDetail memberDetail = new MemberDetail
            {
                MemberDetailId = Guid.NewGuid(),
                WorkSpaceId = Guid.Parse(model.WSId),
                Role = "Member",
                Status = "Accepted",
                UserId = user.UserId
            };
           await _context.MemberDetails.AddAsync(memberDetail);
           await _context.SaveChangesAsync();

            return RedirectToAction("IntoWS", new { id = model.WSId });
        }
    }
}
