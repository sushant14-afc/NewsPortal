using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsPortal.Data;
using NewsPortal.DTOs;
using NewsPortal.Entity;
using Microsoft.AspNetCore.Identity;


namespace NewsPortal.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterUsersController : ControllerBase
    {
        private readonly DataContext _context;

        public RegisterUsersController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("User")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDtos user)
        {
            var existingUser = await _context.RegisterUsers.FirstOrDefaultAsync<RegisterUser>(i => i.Email  == user.Email);

            if (existingUser !=null)
            {
                return BadRequest("User with that email already exist");
            }

            if(user.Password != user.ConfirmPassword)
            {
                return BadRequest("Password do not match");
            }

            var passwordHasher = new PasswordHasher<RegisterUser>();
             
            var newUser = new RegisterUser
            {
                Email = user.Email,
            };

            newUser.Password = passwordHasher.HashPassword(newUser, user.Password);

            _context.RegisterUsers.Add(newUser);
            await _context.SaveChangesAsync();  
            return Ok("User Registered Successfully");
        }

        [HttpGet("getUser/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.RegisterUsers.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UpdateInfo updateUserInfo, [FromRoute] int id)
        {
            var existingUser = await _context.RegisterUsers.FindAsync(id);
            
            if (existingUser == null)
            {
                return BadRequest("User not found");
            }

            var passwordHasher = new PasswordHasher<RegisterUser>();

            var passwordCheck = passwordHasher.VerifyHashedPassword(existingUser, existingUser.Password, updateUserInfo.CurrentPassword);
            if (passwordCheck != PasswordVerificationResult.Success)
            {
                return BadRequest("Current password is incorrect.");
            }

            // Update email if provided
            if (!string.IsNullOrWhiteSpace(updateUserInfo.Email))
            {
                existingUser.Email = updateUserInfo.Email;
            }

            // Update password if provided
            if (!string.IsNullOrWhiteSpace(updateUserInfo.NewPassword))
            {
                existingUser.Password = passwordHasher.HashPassword(existingUser, updateUserInfo.NewPassword);
            }

            await _context.SaveChangesAsync();

            return Ok("User info updated successfully.");
        }
    }
    
}
