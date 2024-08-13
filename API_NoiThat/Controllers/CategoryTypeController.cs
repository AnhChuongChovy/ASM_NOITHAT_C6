using API_NoiThat.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_NoiThat.Models;
using System;
using System.IO;

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

        [HttpGet("categorytypes")]
        public async Task<ActionResult<IEnumerable<CategoryType>>> GetCategoryTypes()
        {
            var categoryTypes = await _context.CategoryType.ToListAsync();
            return Ok(categoryTypes);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryType>>> CategoryType(int pageIndex = 0, int pageSize = 10, string searchTerm = "")
        {
            var query = _context.CategoryType.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p => p.TenLoaiDanhMuc.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            var totalProducts = await query.CountAsync();
            var products = await query
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            Response.Headers.Add("X-Total-Count", totalProducts.ToString());

            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryType>> PostCategoryType(CategoryType categoryType)
        {
            if (categoryType == null)
            {
                return BadRequest();
            }

            _context.CategoryType.Add(categoryType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoryType", new { id = categoryType.ID }, categoryType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoryType(int id, CategoryType categoryType)
        {
            if (id != categoryType.ID)
            {
                return BadRequest();
            }

            _context.Entry(categoryType).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryType>> GetCategoryType(int id)
        {
            var categoryType = await _context.CategoryType.FindAsync(id);
            if (categoryType == null)
            {
                return NotFound();
            }

            return categoryType;
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
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
