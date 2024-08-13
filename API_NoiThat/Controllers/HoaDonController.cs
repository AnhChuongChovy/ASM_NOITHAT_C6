using API_NoiThat.Data;
using API_NoiThat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


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
        public async Task<ActionResult<Bill>> GetBill(int id)
        {
            var bill = await _context.Bill
                .Include(b => b.BillDetail) // Load BillDetail information
                .FirstOrDefaultAsync(b => b.ID == id);

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


        [HttpGet("by-bill/{billId}")]
        public async Task<ActionResult<IEnumerable<BillDetail>>> GetBillDetailsByBillId(int billId)
        {
            var billDetails = await _context.BillDetail
                .Where(bd => bd.IDHoaDon == billId)
                .Include(bd => bd.Product) // Load Product information
                .ToListAsync();

            if (billDetails == null || !billDetails.Any())
            {
                return NotFound();
            }

            return billDetails;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBillStatus(int id, [FromBody] string newStatus)
        {
            var bill = await _context.Bill.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }

            bill.TrangThai = newStatus;
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
