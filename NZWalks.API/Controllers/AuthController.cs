using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Username
            };

            var IdentityResult = await userManager.CreateAsync(identityUser, registerDto.Password);

            if (IdentityResult.Succeeded)
            {
                // Add roles to the user
                if (registerDto.Roles != null && registerDto.Roles.Any())
                {
                    var roles = registerDto.Roles.Split(',').Select(role => role.Trim());
                    IdentityResult = await userManager.AddToRolesAsync(identityUser, roles);
                    if (IdentityResult.Succeeded)
                    {
                        return Ok();
                    }
                }
                
            }

            

            return BadRequest(IdentityResult.Errors);
        }
    }
}
