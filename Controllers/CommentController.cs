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
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly AuctionContext _context;

        public CommentController(AuctionContext context)
        {
            _context = context;
        }

        // GET: api/comment
        [HttpGet]
        public IActionResult GetComments()
        {
            var comments = _context.Comments.ToList();
            return Ok(new ApiResponse<IEnumerable<Comment>>(comments));
        }

        // GET: api/comment/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment(Guid id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound(new ApiResponse<object>("Comment not found"));
            }

            return Ok(new ApiResponse<Comment>(comment));
        }

        // POST: api/comment
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>("Invalid model state"));
            }

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { id = comment.CommentId }, new ApiResponse<Comment>(comment));
        }

        // PUT: api/comment/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(Guid id, [FromBody] Comment updatedComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>("Invalid model state"));
            }

            if (id != updatedComment.CommentId)
            {
                return BadRequest(new ApiResponse<object>("Invalid comment ID"));
            }

            _context.Entry(updatedComment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound(new ApiResponse<object>("Comment not found"));
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/comment/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound(new ApiResponse<object>("Comment not found"));
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentExists(Guid id)
        {
            return _context.Comments.Any(c => c.CommentId == id);
        }
    }
}
