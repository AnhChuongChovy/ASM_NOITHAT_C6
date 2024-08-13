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
        private readonly IConfiguration _configuration;
        public AccountController(ApplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            var account = await _context.Account.ToListAsync();
            return Ok(account);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccountId(int id)
        {
            var account = await _context.Account.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        [HttpPost("login")]
        public async Task<ActionResult<Account>> Login([FromBody] LoginModel model)
        {
            var account = await _context.Account
                .Include(a => a.Role)
                .FirstOrDefaultAsync(a => a.Email == model.Email);

            if (account == null)
            {
                return Unauthorized();
            }

            // So sánh mật khẩu đã mã hóa
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, account.MatKhau);
            if (!isPasswordValid)
            {
                return Unauthorized();
            }

            return Ok(new
            {
                account.ID,
                account.TenNguoiDung,
                account.Email,
                account.IDRole
            });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, Account account)
        {
            if (id != account.ID)
            {
                return BadRequest();
            }


            _context.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool AccountExists(int id)
        {
            return _context.Account.Any(e => e.ID == id);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Kiểm tra email đã tồn tại chưa
            var existingUser = await _context.Account.FirstOrDefaultAsync(u => u.Email == account.Email);
            if (existingUser != null)
            {
                return Conflict("Email đã được sử dụng.");
            }

            // Mã hóa mật khẩu (có thể dùng thư viện BCrypt hoặc Identity)
            account.MatKhau = BCrypt.Net.BCrypt.HashPassword(account.MatKhau);
            account.IDRole = 2;
            // Lưu thông tin người dùng vào database
            _context.Account.Add(account);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Đăng ký thành công." });
        }

        //Up hình
        [HttpPost("upload-image")]
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

        //Đổi mật khẩu
        [HttpPost("ChangePassword/{id}")]
        public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordModel model)
        {
            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                return NotFound("User not found.");
            }

            // Kiểm tra mật khẩu hiện tại
            if (!BCrypt.Net.BCrypt.Verify(model.CurrentPassword, account.MatKhau))
            {
                return BadRequest("Mật khẩu hiện tại không chính xác.");
            }

            // Mã hóa mật khẩu mới
            account.MatKhau = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);

            _context.Account.Update(account);
            await _context.SaveChangesAsync();

            return Ok("Password changed successfully.");
        }

        public class ChangePasswordModel
        {
            public string CurrentPassword { get; set; }
            public string NewPassword { get; set; }
            public string ConfirmPassword { get; set; }
        }

    }
}
