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
    [Authorize] // Requires authentication for all actions in this controller
    [ApiController]
    [Route("api/[controller]")]
    public class AuctionController : ControllerBase
    {
        private readonly AuctionContext _context;

        public AuctionController(AuctionContext context)
        {
            _context = context;
        }

        // GET: api/auction
        [HttpGet]
        public IActionResult GetAuctions()
        {
            var auctions = _context.Auctions.ToList();
            return Ok(auctions);
        }

        // GET: api/auction/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuction(Guid id)
        {
            var auction = await _context.Auctions.FindAsync(id);

            if (auction == null)
            {
                return NotFound();
            }

            return Ok(auction);
        }

        // POST: api/auction
        [HttpPost]
        public async Task<IActionResult> CreateAuction([FromBody] Auction auction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Auctions.Add(auction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuction", new { id = auction.AuctionId }, auction);
        }

        // PUT: api/auction/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuction(Guid id, [FromBody] Auction updatedAuction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updatedAuction.AuctionId)
            {
                return BadRequest("Invalid auction ID");
            }

            _context.Entry(updatedAuction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuctionExists(id))
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

        // DELETE: api/auction/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuction(Guid id)
        {
            var auction = await _context.Auctions.FindAsync(id);

            if (auction == null)
            {
                return NotFound();
            }

            _context.Auctions.Remove(auction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuctionExists(Guid id)
        {
            return _context.Auctions.Any(a => a.AuctionId == id);
        }
    }
}
