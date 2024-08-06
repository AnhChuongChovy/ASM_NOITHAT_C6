using API_NoiThat.Data;
using API_NoiThat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAsemly_NoiThat.Model;

namespace API_NoiThat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public HoaDonController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Bill>>> GetBills()
        {
            var bills = await _context.Bill.ToListAsync();
            return Ok(bills);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Bill>> GetBillId(int id)
        {
            var bill = await _context.Bill.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }

            return bill;
        }
        
        [HttpPost]
        public async Task<ActionResult<Models.Bill>> PostBill(Models.Bill bill)
        {
            if (bill == null)
            {
                return BadRequest();
            }

            _context.Bill.Add(bill);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBillId", new { id = bill.ID }, bill);
        }
    }
}
