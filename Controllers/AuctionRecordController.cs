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
    public class AuctionRecordController : ControllerBase
    {
        private readonly AuctionContext _context;

        public AuctionRecordController(AuctionContext context)
        {
            _context = context;
        }

        // GET: api/auctionrecord
        [HttpGet]
        public IActionResult GetAuctionRecords()
        {
            var auctionRecords = _context.AuctionRecords.ToList();
            return Ok(new ApiResponse<IEnumerable<AuctionRecord>>(auctionRecords));
        }

        // GET: api/auctionrecord/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuctionRecord(Guid id)
        {
            var auctionRecord = await _context.AuctionRecords.FindAsync(id);

            if (auctionRecord == null)
            {
                return NotFound(new ApiResponse<object>("Auction record not found"));
            }

            return Ok(new ApiResponse<AuctionRecord>(auctionRecord));
        }

        // POST: api/auctionrecord
        [HttpPost]
        public async Task<IActionResult> CreateAuctionRecord([FromBody] AuctionRecord auctionRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.AuctionRecords.Add(auctionRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuctionRecord", new { id = auctionRecord.AuctionRecordId }, new ApiResponse<AuctionRecord>(auctionRecord));
        }

        // PUT: api/auctionrecord/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuctionRecord(Guid id, [FromBody] AuctionRecord updatedAuctionRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<object>("Invalid model state"));
            }

            if (id != updatedAuctionRecord.AuctionRecordId)
            {
                return BadRequest(new ApiResponse<object>("Invalid auction record ID"));
            }

            _context.Entry(updatedAuctionRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuctionRecordExists(id))
                {
                    return NotFound(new ApiResponse<object>("Auction record not found"));
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/auctionrecord/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuctionRecord(Guid id)
        {
            var auctionRecord = await _context.AuctionRecords.FindAsync(id);

            if (auctionRecord == null)
            {
                return NotFound(new ApiResponse<object>("Auction record not found"));
            }

            _context.AuctionRecords.Remove(auctionRecord);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuctionRecordExists(Guid id)
        {
            return _context.AuctionRecords.Any(ar => ar.AuctionRecordId == id);
        }
    }
}
