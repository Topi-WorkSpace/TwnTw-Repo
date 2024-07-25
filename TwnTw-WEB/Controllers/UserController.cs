using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwnTw_WEB.Data;
using TwnTw_WEB.DTO_Models;
using TwnTw_WEB.Models;

namespace TwnTw_WEB.Controllers
{
    public class UserController : Controller
    {
        private readonly TwnTwDbContext _DbContext;
        private readonly IMapper _Mapper;
        public UserController(TwnTwDbContext dbContext, IMapper mapper)
        {
            _DbContext = dbContext;
            _Mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateModel userCreateModel)
        {
            await _DbContext.Users.AddAsync(_Mapper.Map<User>(userCreateModel));
            await _DbContext.SaveChangesAsync();
            return View();
        }
        public IActionResult Login() { return View(); }
        [HttpPost]
        public async Task<IActionResult> Login(UserLogin model)
        {
            var user = _DbContext.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

            if (user != null)
            {
                Console.WriteLine("Đăng Nhập thành công");
                HttpContext.Session.SetString("UserName", user.UserName);
                // Đăng nhập thành công
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Đăng nhập thất bại
                ModelState.AddModelError(string.Empty, "Email hoặc mật khẩu không đúng.");
                return View(model);
            }
        }



    }
}
