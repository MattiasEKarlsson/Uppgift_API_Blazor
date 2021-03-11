using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using API.Data;
using API.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SqlDbContext _context;
        private IConfiguration Configuration { get; }

        public UsersController(SqlDbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody]SignUp model)
        {
            try
            {
                var user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email
                };
                user.CreatePassword(model.Password);

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return new OkResult();
            }
            catch 
            {
                return new BadRequestResult();
            }
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignIn user)
        {
            try
            {
                var _user = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

                if (_user != null)
                {
                    if (_user.ValidatePassword(user.Password))
                    {
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var expiresDate = DateTime.Now.AddMinutes(1);

                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            { 
                                new Claim("UserId", _user.Id.ToString()),
                                new Claim("Expires", expiresDate.ToString())
                            }),
                            Expires = expiresDate,
                            SigningCredentials = new SigningCredentials
                            (new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("SecretKey").Value)), SecurityAlgorithms.HmacSha512Signature)
                        };

                        var _accessToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
                        return new OkObjectResult(new {AccessToken = _accessToken });
                    }

                }

            }
            catch { }

            return new BadRequestResult();
        }
    }
}
