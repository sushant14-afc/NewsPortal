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
    public class LoginController : ControllerBase
    {
        private DataContext _context;

        public LoginController(DataContext context)
        {
            _context = context;
        }

            [HttpPost("loginUser")]
            public async Task<IActionResult> Login([FromBody] LoginDtos loginDto)
            {
                var user = await _context.RegisterUsers.FirstOrDefaultAsync<RegisterUser>(i => i.Email == loginDto.Email);

                if(user == null)
                {
                //return NotFound("Email Not Registered");
                return NotFound(new { message = "Email Not Registered" });
            }
                var passwordHasher = new PasswordHasher<RegisterUser>();
                var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, loginDto.Password);

                if (passwordVerificationResult == PasswordVerificationResult.Failed)
                {
                //return Unauthorized("Invalid credentials.");
                return Unauthorized(new { message = "Invalid credentials." });

            }

            //return Ok("Login Sucessfull");
            //return Ok(new { message = "Login Successful", user = new { user.Id, user.Email } });
                return Ok(new { user.Id, user.Email });


        }



    }
}
