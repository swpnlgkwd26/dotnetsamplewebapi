using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using sample_api.Models;
using sample_api.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace sample_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }
        [HttpPost]
        public IActionResult Login([FromBody] LoginViewModel loginViewModel)
        {
            if (loginViewModel == null)
            {
                return BadRequest("Login Information Cant be null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Model State is Invalid");
            }
            if (UserExists(loginViewModel).Result)
            {
                // Generate Token  :  Header + Payload  +  Sign
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecucretKey@845"));
                var signIncredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                // Payload = claims
                var claims = new[]
                {
                    new Claim("Email",loginViewModel.Email),
                    new Claim(ClaimTypes.Role,"Users"),
                    new Claim(JwtRegisteredClaimNames.Iat,DateTime.Now.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub,"ProductAPIServiceAccessToken")
                };

                // Configure Token 
                var tokenOptions = new JwtSecurityToken(issuer: "http://localhost:5001",
                    audience: "http://localhost:5001",
                    claims: claims,
                    expires: DateTime.Now.AddSeconds(240),
                    signingCredentials: signIncredentials);

                // Generate the token
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Token = tokenString});
            }
            else
            {
                return Unauthorized();
            }

        }
        // Validating the User
        private async Task<bool> UserExists(LoginViewModel loginViewModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, false, false);
            return result.Succeeded == true ? true : false;
        }
    }
}
