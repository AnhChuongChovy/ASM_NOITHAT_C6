using API_NoiThat.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppBlazor.Models;

namespace API_NoiThat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryTypeController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public CategoryTypeController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryType>>> GetCategoryTypes()
        {
            return await _context.CategoryType.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<CategoryType>> PostCategoryType(CategoryType catetype)
        {
            if (!_context.Category.Any(c => c.ID == catetype.IDDanhMuc))
            {
                return BadRequest("Invalid Category ID");
            }

            _context.CategoryType.Add(catetype);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategoryTypeById), new { id = catetype.ID }, catetype);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryType>> GetCategoryTypeById(int id)
        {
            var categoryType = await _context.CategoryType.FindAsync(id);

            if (categoryType == null)
            {
                return NotFound();
            }

            return categoryType;
        }
    }
}
