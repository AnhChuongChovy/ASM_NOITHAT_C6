using API_NoiThat.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppBlazor.Models;

namespace API_NoiThat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public AccountController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetProducts()
        {
            return await _context.Account.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetProductId(int id)
        {
            var account = await _context.Account.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }
    }
}
