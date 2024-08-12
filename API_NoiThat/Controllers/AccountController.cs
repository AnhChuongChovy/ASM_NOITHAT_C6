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

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        //{
        //    return await _context.Account.ToListAsync();
        //}

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
                .FirstOrDefaultAsync(a => a.Email == model.Email && a.MatKhau == model.Password);

            if (account == null)
            {
                return Unauthorized(); // Trả về mã lỗi 401 nếu thông tin đăng nhập không chính xác
            }

            // Trả về thông tin người dùng và token (nếu sử dụng token)
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

        
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Account user)
        {
            if (user == null || string.IsNullOrEmpty(user.TenNguoiDung) || string.IsNullOrEmpty(user.MatKhau) || string.IsNullOrEmpty(user.Email))
            {
                return BadRequest("Invalid user data.");
            }

            _context.Account.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccountId", new { id = user.ID }, user);
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
