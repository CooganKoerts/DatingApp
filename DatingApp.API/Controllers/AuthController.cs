using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.DTOs;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository repo;
        public AuthController(IAuthRepository repo) 
        {
            this.repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO userForRegisterDTO) 
        {
            // Validate request 

            userForRegisterDTO.Username = userForRegisterDTO.Username.ToLower();

            if (await this.repo.UserExists(userForRegisterDTO.Username))
                return BadRequest("Username already exists");

            var userToCreate = new User 
            {
                Username = userForRegisterDTO.Username
            };

            var createdUser = await this.repo.Register(userToCreate, userForRegisterDTO.Password);

            return StatusCode(201);     
        }    
    }
}