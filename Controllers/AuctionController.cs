using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuctionBackend.Models;
using Microsoft.AspNet.Identity;

namespace AuctionBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly AuctionContext _context;

        public AuctionController(AuctionContext context)
        {
            _context = context;
        }

        // GET: api/Auction
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Auction>>> GetAuctions()
        {
            return await _context.Auctions.ToListAsync();
        }

        // GET: api/Auction/5
        [HttpGet("{userId}/{auctionId}")]
        public async Task<ActionResult<Auction>> GetAuction(int auctionId)
        {
            
                var auction = await _context.Auctions.FindAsync(auctionId);

                if (auction != null)
                {
                    return auction;
                }
          
            return NotFound();
        }

        // POST: api/Auction
        [HttpPost]
        public async Task<IActionResult> CreateAuction([FromBody] Auction auctionCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Get the current user ID from the authenticated user
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var auction = new Auction
            {
                Title = auctionCreateModel.Title,
                Description = auctionCreateModel.Description,
                StartingPrice = auctionCreateModel.Price,
                EndTime = auctionCreateModel.ExpiryDate,
                UserId = userId, // Assign the auction to the current user
                CategoryId = auctionCreateModel.CategoryId // Assuming CategoryId is provided in the request
            };

            _context.Auctions.Add(auction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuction", new { id = auction.AuctionId }, auction);
        }

        // DELETE: api/Auction/5
        [HttpDelete("{userId}/{user}")]
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
    }
}