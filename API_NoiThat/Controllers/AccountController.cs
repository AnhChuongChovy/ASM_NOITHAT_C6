using API_NoiThat.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using API_NoiThat.Models;
using Microsoft.Extensions.Configuration;
using System.Linq;

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
                account.Role.NameRole
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] Account updatedAccount)
        {
            var account = await _context.Account.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            account.TenNguoiDung = updatedAccount.TenNguoiDung;
            account.Email = updatedAccount.Email;
            account.DiaChi = updatedAccount.DiaChi;
            account.SDT = updatedAccount.SDT;
            account.GioiTinh = updatedAccount.GioiTinh;
            account.HinhAnh = updatedAccount.HinhAnh;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
