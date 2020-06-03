using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstrumentsShopAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InstrumentsShopAPI.Controllers
{
    [Route("api/[controller]")]
    public class RegisterController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<CustomUserIdentity> _userManager;

        public RegisterController(DatabaseContext context, UserManager<CustomUserIdentity> manager)
        {
            _context = context;
            _userManager = manager;
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody] AuthRequestModel authRequestModel)
        {
            var login = authRequestModel.Login;
            var password = authRequestModel.Password;

            if (_context.Users.Where(u => u.UserName == login).ToArray().Length == 0)
            {
                var user = new CustomUserIdentity();
                user.UserName = login;
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, password);

                _context.Users.Add(user);
                _context.SaveChanges();

                return "User: " + user.UserName + " added";
            }
            else
            {
                return BadRequest("User allready exist in Database");
            }
        }
    }
}
