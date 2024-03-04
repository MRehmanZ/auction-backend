using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuctionBackend.Models;

namespace AuctionBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuctionContext _context;

        public UserController(UserManager<ApplicationUser> userManager, AuctionContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userManager.Users.ToList();
            return Ok(new ApiResponse<IEnumerable<ApplicationUser>>(users));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return NotFound(new ApiResponse<ApplicationUser>("User not found"));
            }

            return Ok(new ApiResponse<ApplicationUser>(user));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                return CreatedAtAction("GetUser", new { id = user.Id }, new ApiResponse<ApplicationUser>(user));
            }

            return BadRequest(result.Errors);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] ApplicationUser updatedUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model state");
            }

            if (id != updatedUser.Id)
            {
                return BadRequest(new ApiResponse<object>("Invalid user ID"));
            }

            _context.Entry(updatedUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound(new ApiResponse<object>("User not found"));
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return NotFound(new ApiResponse<object>("User not found"));
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest(result.Errors);
        }

        private bool UserExists(Guid id)
        {
            return _userManager.Users.Any(u => u.Id == id);
        }
    }

    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public ApiResponse(T data)
        {
            Data = data;
        }

        public ApiResponse(string message)
        {
            Message = message;
        }

        public ApiResponse(string message, IEnumerable<string> errors)
        {
            Message = message;
            Errors = errors;
        }
    }
}
