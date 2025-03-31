using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
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

                    IdentityResult = await userManager.AddToRolesAsync(identityUser, registerDto.Roles);
                    if (IdentityResult.Succeeded)
                    {
                        return Ok("User Register! Please login");
                    }
                }

            }
            return BadRequest(IdentityResult.Errors);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    //GEt ROles for this user
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    { 
                        //Create Token
                        var jwtToken = tokenRepository.CreateJwtToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(response);
                    }
                 
                }
            }
            return BadRequest("Invalid Username or Password");
        }
    }
}
