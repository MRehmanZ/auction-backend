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
   // [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuctionContext _context;
        private readonly ILogger<UserController> _logger;

        public UserController(UserManager<ApplicationUser> userManager, AuctionContext context, ILogger<UserController> logger)
        {
            _userManager = userManager;
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUsers()
        {
            var users = _context.Users.ToList();
            _logger.LogInformation("Getting list of all users");

            return Ok(new ApiResponse<IEnumerable<ApplicationUser>>(users));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            _logger.LogInformation("Getting user with User ID: {UserId}", id.ToString());
            if (user == null)
            {
                _logger.LogInformation("User: {UserId} not found", id.ToString());
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

            user.UserName = user.Email;

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created with ID: {UserID}", user.Id);
                return CreatedAtAction("GetUser", new { id = user.Id }, new ApiResponse<ApplicationUser>(user));
            }

            _logger.LogError("Error creating user");
            return BadRequest(result.Errors);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] ApplicationUser updatedUser)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Error invalid model state");
                return BadRequest("Invalid model state");
            }

            if (id != updatedUser.Id)
            {
                _logger.LogError("Error updating user: {UserId}", id.ToString());

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
                    _logger.LogWarning("User not found");
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
                _logger.LogWarning("User not found");

                return NotFound(new ApiResponse<object>("User not found"));
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                _logger.LogInformation("User deleted with ID: {UserID}", user.Id.ToString());

                return NoContent();
            }

            return BadRequest(result.Errors);
        }

        private bool UserExists(Guid id)
        {
            bool IsFound = _userManager.Users.Any(u => u.Id == id);
            
            if (IsFound)
            {
                _logger.LogInformation("User found with ID: {UserID}", id.ToString());

            } 
            return IsFound;
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
