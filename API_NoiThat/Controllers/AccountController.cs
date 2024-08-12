using API_NoiThat.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using API_NoiThat.Models;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.IO;

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

            return NoContent();
        }

        private bool AccountExists(int id)
        {
            return _context.Account.Any(e => e.ID == id);
        }

        
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Account user)
        {
            if (user == null || string.IsNullOrEmpty(user.TenNguoiDung) || string.IsNullOrEmpty(user.MatKhau) || string.IsNullOrEmpty(user.Email))
            {
                return BadRequest("Invalid user data.");
            }

            _context.Account.Update(account);
            await _context.SaveChangesAsync();

            return Ok("Đổi mật khẩu thành công");
        }

        [HttpPost("upload-account")]
        public async Task<IActionResult> UploadImageAccount(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image_SP_ASM");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var filePath = Path.Combine(folderPath, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileUrl = $"{Request.Scheme}://{Request.Host}/Image_SP_ASM/{file.FileName}";

            return Ok(new { FileUrl = fileUrl });
        }

    }
}
