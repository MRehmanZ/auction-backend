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
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            _logger.LogInformation("Getting user with User ID: {UserId}", id);

            if (user == null)
            {
                _logger.LogInformation("User: {UserId} not found", id);
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
        public async Task<IActionResult> UpdateUser(string id, [FromBody] ApplicationUser updatedUser)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Error invalid model state");
                return BadRequest("Invalid model state");
            }

            if (!id.Equals(updatedUser.Id.ToString()))
            {
                _logger.LogError("Error updating user: {UserId}", id);

                return BadRequest(new ApiResponse<object>("Invalid user ID"));
            }

            var currentUser = await _userManager.FindByIdAsync(id);

            if (currentUser != null) 
            {
                currentUser.Email = updatedUser.Email;
                currentUser.Password = updatedUser.Password;
                currentUser.Balance = updatedUser.Balance;

                var result = await _userManager.UpdateAsync(currentUser);

                try
                {
                    if (result.Succeeded)
                    {
                        Ok(updatedUser);
                    }
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
            } else
            {
                NotFound("User not found");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

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

        private bool UserExists(string id)
        {
            bool IsFound = _userManager.Users.Any(u => u.Id.ToString() == id);
            
            if (IsFound)
            {
                _logger.LogInformation("User found with ID: {UserID}", id);

            } 
            return IsFound;
        }
    }


    // Generating response in JSON format
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
