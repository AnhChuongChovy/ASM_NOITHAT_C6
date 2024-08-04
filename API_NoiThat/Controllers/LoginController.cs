using API_NoiThat.Data;
using API_NoiThat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using static WebAsemly_NoiThat.Service.LoginService;

namespace API_NoiThat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public LoginController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            //Xét email và mật khẩu và role khi đăng nhập
            var account = await _context.Account
                .Include(a => a.Role)
                .FirstOrDefaultAsync(a => a.Email == request.Email && a.MatKhau == request.MatKhau);

            if (account == null)
            {
                return Unauthorized();
            }

            //Lưu email vs role vào session 
            HttpContext.Session.SetString("user", account.Email);
            HttpContext.Session.SetString("role", account.IDRole.ToString());
            HttpContext.Session.SetInt32("userId", account.ID);

            return Ok(new { role = account.IDRole.ToString(), userId = account.ID });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            //Đăng xuất
            HttpContext.Session.Clear();
            return Ok();
        }

        [HttpGet("user")]
        public IActionResult GetUser()
        {
            var user = HttpContext.Session.GetString("user");
            var role = HttpContext.Session.GetString("role");
            var userId = HttpContext.Session.GetInt32("userId");

            if (user == null)
            {
                return Unauthorized();
            }

            var account = _context.Account
                .FirstOrDefault(a => a.ID == userId);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
            //var user = HttpContext.Session.GetString("user");
            //var role = HttpContext.Session.GetString("role");

            //if (user == null)
            //{
            //    return Unauthorized();
            //}

            //return Ok(new { user, role });
        }
    }
}
