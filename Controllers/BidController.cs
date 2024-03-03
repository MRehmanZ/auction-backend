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
    public class BidController : ControllerBase
    {
        private readonly AuctionContext _context;

        public BidController(AuctionContext context)
        {
            _context = context;
        }

        // GET: api/bid
        [HttpGet]
        public IActionResult GetBids()
        {
            var bids = _context.Bids.ToList();
            return Ok(new ApiResponse<IEnumerable<Bid>>(bids));
        }

        // GET: api/bid/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBid(Guid id)
        {
            var bid = await _context.Bids.FindAsync(id);

            if (bid == null)
            {
                return NotFound(new ApiResponse<object>("Bid not found"));
            }

            return Ok(new ApiResponse<Bid>(bid));
        }

        // POST: api/bid
        [HttpPost]
        public async Task<IActionResult> CreateBid([FromBody] Bid bid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model state");
            }

            // Check if the bid is higher than the current highest bid
            var currentHighestBid = await _context.Bids
                .Where(b => b.AuctionId == bid.AuctionId)
                .OrderByDescending(b => b.Price)
                .FirstOrDefaultAsync();

            if (currentHighestBid != null && bid.Price <= currentHighestBid.Price)
            {
                return BadRequest(new ApiResponse<object>("Bid Price must be higher than the current highest bid"));
            }

            // Update the current highest bid
            if (currentHighestBid == null || bid.Price > currentHighestBid.Price)
            {
                // Update the auction's current highest bid
                var auction = await _context.Auctions.FindAsync(bid.AuctionId);
                auction.CurrentHighestBid = bid.Price;
                _context.Entry(auction).State = EntityState.Modified;
            }

            _context.Bids.Add(bid);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBid", new { id = bid.BidId }, new ApiResponse<Bid>(bid));
        }

        // PUT: api/bid/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBid(Guid id, [FromBody] Bid updatedBid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model state");
            }

            if (id != updatedBid.BidId)
            {
                return BadRequest(new ApiResponse<object>("Invalid bid ID"));
            }

            _context.Entry(updatedBid).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BidExists(id))
                {
                    return NotFound(new ApiResponse<object>("Bid not found"));
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/bid/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBid(Guid id)
        {
            var bid = await _context.Bids.FindAsync(id);

            if (bid == null)
            {
                return NotFound(new ApiResponse<object>("Bid not found"));
            }

            _context.Bids.Remove(bid);
            await _context.SaveChangesAsync();

            // Update the auction's current highest bid after deleting a bid
            UpdateCurrentHighestBid(bid.AuctionId);

            return NoContent();
        }

        private void UpdateCurrentHighestBid(Guid auctionId)
        {
            var currentHighestBid = _context.Bids
                .Where(b => b.AuctionId == auctionId)
                .OrderByDescending(b => b.Price)
                .FirstOrDefault();

            var auction = _context.Auctions.Find(auctionId);
            auction.CurrentHighestBid = currentHighestBid?.Price ?? 0;
            _context.Entry(auction).State = EntityState.Modified;
            _context.SaveChanges();
        }

        private bool BidExists(Guid id)
        {
            return _context.Bids.Any(b => b.BidId == id);
        }
    }
}
