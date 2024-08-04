using API_NoiThat.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using API_NoiThat.Models;
using WebAsemly_NoiThat.Model;

namespace API_NoiThat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountController(ApplicationDBContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Account>>> GetAccounts()
        {
            return await _context.Account.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Account>> GetAccountId(int id)
        {
            var account = await _context.Account.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> DoiMatKhau([FromBody] ChangePasswordRequest request)
        {
            var userIdString = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return Unauthorized("Người dùng chưa đăng nhập");
            }

            var account = await _context.Account.FindAsync(userId);
            if (account == null)
            {
                return NotFound("Tài khoản không tìm thấy");
            }

            if (account.MatKhau != request.CurrentPassword)
            {
                return BadRequest("Mật khẩu hiện tại không chính xác");
            }

            if (request.NewPassword != request.ConfirmNewPassword)
            {
                return BadRequest("Mật khẩu mới và xác nhận mật khẩu không khớp.");
            }

            account.MatKhau = request.NewPassword; // Trong thực tế, bạn nên mã hóa mật khẩu trước khi lưu

            _context.Account.Update(account);
            await _context.SaveChangesAsync();

            return Ok("Đổi mật khẩu thành công");
        }
    }
}
