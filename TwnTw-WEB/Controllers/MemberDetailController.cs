using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwnTw_WEB.Data;
using TwnTw_WEB.Models;

namespace TwnTw_WEB.Controllers
{
    public class MemberDetailController : Controller
    {
        private readonly TwnTwDbContext _context;
        public MemberDetailController(TwnTwDbContext context)
        {
            _context = context;
        }

        //Hiển thị 
        [HttpGet]
        public async Task<IActionResult> ListMemberDetail(Guid id) //WSid
        {
           
            IEnumerable<MemberDetail> memberDetails = await _context.MemberDetails.Where(a => a.WorkSpaceId == id).ToListAsync();
            
            HttpContext.Session.SetString("WorkSpaceId", id.ToString());
            return View(memberDetails);
        }



        [HttpGet]
        public async Task<IActionResult> CreateMemberDetail() 
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateMemberDetail(string email)
        {
            
            User user = await _context.Users.FirstOrDefaultAsync(a => a.Email == email);
            if(user != null)
            {
                MemberDetail memberDetail = new MemberDetail
                {
                    MemberDetailId = Guid.NewGuid(),
                    WorkSpaceId = Guid.Parse(HttpContext.Session.GetString("WorkSpaceId")),
                    Role = "Member",
                    Status = "Waiting",
                    UserId = user.UserId
                };
                await _context.MemberDetails.AddAsync(memberDetail);
                await _context.SaveChangesAsync();
                Console.WriteLine("Mời thành công");
                return RedirectToAction("ListWorkspace", "Workspace");
            }

            Console.WriteLine("Mời không thành công");
            return View();
            
        }

        //Xoá member detail
        [HttpGet]
        public async Task<IActionResult> DeleteMemberDetail(Guid id)
        {
            MemberDetail user = await _context.MemberDetails.FirstOrDefaultAsync(a => a.MemberDetailId == id);
            _context.MemberDetails.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("ListMemberDetail", new { id = user.WorkSpaceId });
        }
    }
}
