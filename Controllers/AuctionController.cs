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
            return Ok(new ApiResponse<IEnumerable<Auction>>(auctions));
        }

        // GET: api/auction/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuction(Guid id)
        {
            var auction = await _context.Auctions.FindAsync(id);

            if (auction == null)
            {
                return NotFound(new ApiResponse<object>("Auction not found"));
            }

            return Ok(new ApiResponse<Auction>(auction));
        }

        // POST: api/auction
        [HttpPost]
        public async Task<IActionResult> CreateAuction([FromBody] Auction auction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>("Invalid model state"));
            }

            // Initialize properties
            auction.CurrentHighestBid = 0;
            auction.WinnerBidId = null;

            _context.Auctions.Add(auction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuction", new { id = auction.AuctionId }, new ApiResponse<Auction>(auction));
        }

        // PUT: api/auction/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuction(Guid id, [FromBody] Auction updatedAuction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model state");
            }

            if (id != updatedAuction.AuctionId)
            {
                return BadRequest(new ApiResponse<object>("Invalid auction ID"));
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
                    return NotFound(new ApiResponse<object>("Auction not found"));
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
                return NotFound(new ApiResponse<object>("Auction not found"));
            }

            _context.Auctions.Remove(auction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/auction/{id}/place-bid
        [HttpPost("{id}/place-bid")]
        public async Task<IActionResult> PlaceBid(Guid id, [FromBody] Bid bid)
        {
            var auction = await _context.Auctions.FindAsync(id);

            if (auction == null)
            {
                return NotFound(new ApiResponse<object>("Auction not found"));
            }

            // Check if the bid Price is higher than the current highest bid
            if (bid.Price <= auction.CurrentHighestBid)
            {
                return BadRequest(new ApiResponse<object>("Bid Price must be higher than the current highest bid"));
            }

            // Update the current highest bid
            auction.CurrentHighestBid = bid.Price;

            // Assign the bid as the winner bid
            auction.WinnerBidId = bid.BidId;

            // Update the auction
            _context.Entry(auction).State = EntityState.Modified;

            // Save changes to the database
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse<object>("Bid placed successfully"));
        }

        private bool AuctionExists(Guid id)
        {
            return _context.Auctions.Any(a => a.AuctionId == id);
        }
    }
}
