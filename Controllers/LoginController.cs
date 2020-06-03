using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using InstrumentsShopAPI.Models;
using InstrumentsShopAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace InstrumentsShopAPI.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<CustomUserIdentity> _userManager;

        public LoginController(DatabaseContext context, UserManager<CustomUserIdentity> manager)
        {
            _context = context;
            _userManager = manager;
        }

        [HttpPost]
        public ActionResult<String> Post([FromBody] AuthRequestModel authRequestModel, [FromServices] IJwtSigningEncodingKey signingEncodingKey)
        {
            var login = authRequestModel.Login;
            var password = authRequestModel.Password;

            var user = _context.Users.FirstOrDefault(u => u.UserName == login);

            if (user != null)
            {
                var result = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
                if (result == PasswordVerificationResult.Success)
                {
                    var claims = new Claim[]
                    {
                        new Claim(ClaimTypes.Name, login),
                    };

                    var token = new JwtSecurityToken(
                        issuer: "MyShop",
                        audience: "MyShopClient",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: new SigningCredentials(
                            signingEncodingKey.GetKey(),
                            signingEncodingKey.SigningAlgorithm)
                    );

                    string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                    return jwtToken;
                }
                else
                {
                    return BadRequest("Password incorrect");
                }
            }
            else
            {
                return BadRequest("User is not found");
            }
        }
    }
}
