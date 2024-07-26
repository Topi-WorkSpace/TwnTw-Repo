using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwnTw_WEB.Data;
using TwnTw_WEB.DTO_Models;
using TwnTw_WEB.Models;
using BCrypt.Net;
using System.Security.Cryptography;

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

        //[HttpGet]
        //public IActionResult ChangePassword()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult ChangePassword(Guid id, string oldPassword, string newPassword)
        //{
        //    // Find the user by ID
        //    var user = _DbContext.Users.FirstOrDefault(u => u.UserId == id);

        //    if (user == null)
        //    {
        //        return NotFound(); // Or handle the error appropriately
        //    }

        //    // Verify old password (replace with your password hashing logic)
        //    var hashedOldPassword = HashPassword(oldPassword);
        //    if (hashedOldPassword != user.Password)
        //    {
        //        return BadRequest("Incorrect old password");
        //    }

        //    // Hash the new password
        //    var hashedNewPassword = HashPassword(newPassword);
        //    user.Password = hashedNewPassword;

        //    _DbContext.SaveChanges();

        //    return Ok("Password changed successfully");
        //}




        // LOGIN + REGISTER
        public IActionResult Login() { return View(); }
        [HttpPost]
        public async Task<IActionResult> Login(UserLogin model)
        {
            var user = await _DbContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user != null)
            {
                // So sánh mật khẩu đã nhập với mật khẩu đã mã hóa trong cơ sở dữ liệu
                if (BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                {
                    Console.WriteLine("Đăng Nhập thành công");
                    HttpContext.Session.SetString("UserName", user.UserName);
                    // Đăng nhập thành công
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email hoặc mật khẩu không đúng.");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Email hoặc mật khẩu không đúng.");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Kiểm tra email đã tồn tại
                    var existingUser = await _DbContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                    if (existingUser != null)
                    {
                        ModelState.AddModelError("Email", "Email đã tồn tại");
                        return View(model);
                    }
                    // mã hoá mật khẩu
                    var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
                    model.Password = hashedPassword;

                    // Tạo user mới
                    var user = new User
                    {
                        UserId = model.UserId,
                        Email = model.Email,
                        Password = model.Password,
                        UserName = model.UserName
                    };
                    await _DbContext.Users.AddAsync(user);
                    await _DbContext.SaveChangesAsync();

                    return RedirectToAction("Login", "User");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Đã xảy ra lỗi khi đăng ký. Vui lòng thử lại sau.");
                    return View(model);
                }
            }
            return View(model);
        }

        private string HashPassword(string password)
        {
            // Replace with your desired password hashing algorithm
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);

            }
        }

    }
}
