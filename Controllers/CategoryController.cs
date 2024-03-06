using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuctionBackend.Models;

namespace AuctionBackend.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly AuctionContext _context;

        public CategoryController(AuctionContext context)
        {
            _context = context;
        }

        // GET: api/category
        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _context.Categories.ToList();
            return Ok(new ApiResponse<IEnumerable<Category>>(categories));
        }

        // GET: api/category/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(string id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound(new ApiResponse<object>("Category not found"));
            }

            return Ok(new ApiResponse<Category>(category));
        }

        // POST: api/category
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>("Invalid model state"));
            }

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.CategoryId.ToString() }, new ApiResponse<Category>(category));
        }

        // PUT: api/category/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(string id, [FromBody] Category updatedCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model state");
            }

            if (id != updatedCategory.CategoryId.ToString())
            {
                return BadRequest(new ApiResponse<object>("Invalid category ID"));
            }

            _context.Entry(updatedCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound(new ApiResponse<object>("Category not found"));
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/category/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound(new ApiResponse<object>("Category not found"));
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(string id)
        {
            return _context.Categories.Any(c => c.CategoryId.ToString() == id);
        }
    }
}
