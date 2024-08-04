using API_NoiThat.Data;
using API_NoiThat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
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
            var account = await _context.Account
                .Include(a => a.Role)
                .FirstOrDefaultAsync(a => a.Email == request.Email);

            if (account == null || !VerifyPassword(request.MatKhau, account.MatKhau))
            {
                return Unauthorized();
            }

            var user = new User
            {
                Username = account.TenNguoiDung,
                Role = account.Role.ID.ToString(),
            };

            return Ok(user);
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            // Logic to verify password hash
            return passwordHash == password; // Placeholder, replace with your actual password verification logic
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            //Đăng xuất
            HttpContext.Session.Clear();
            return Ok();
        }

       
    }
}
