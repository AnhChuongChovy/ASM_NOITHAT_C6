using API_NoiThat.Data;
using API_NoiThat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using static WebAsemly_NoiThat.Service.RegisterService;


namespace API_NoiThat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public RegisterController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpPost("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingAccount = await _context.Account
                .FirstOrDefaultAsync(a => a.Email == request.Email);

            if (existingAccount != null)
            {
                return Conflict("Email already exists.");
            }

            var newAccount = new Account
            {
                TenNguoiDung = request.TenNguoiDung,
                Email = request.Email,
                MatKhau = request.MatKhau,
                IDRole = 2
            };

            _context.Account.Add(newAccount);
            await _context.SaveChangesAsync();

            return Ok("Account registered successfully.");
        }
    }


    
}
