using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyApiNetCore6.Data;
using MyApiNetCore6.Models;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MyApiNetCore6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly BookDbContext _context;
        private readonly AppSetting _appSetting;
        public UserController(BookDbContext context, IOptionsMonitor<AppSetting> optionsMonitor)
        {
            _context = context;
            _appSetting = optionsMonitor.CurrentValue; // Hoặc dùng IConfiguration`
        }

        [HttpPost("Login")]
        public IActionResult Validate(LoginModel model)
        {
            var user = _context.nguoiDungs.SingleOrDefault(e => e.UserName == model.UserName
             && e.Password == model.Password);
            if (user == null) //Không đúng
            {
                new ApiResponse
                {
                    Success = false,
                    Message = "Invalid User/Password"
                };
            }
            return Ok(
                new ApiResponse
                {
                    Success = true,
                    Message = "Authentication success",
                    Data = GenerateToken(user)
                }
            );
        }

        private string GenerateToken(NguoiDung nguoiDung)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSetting.SecretKey);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, nguoiDung.HoTen),
                    new Claim(ClaimTypes.Email, nguoiDung.Email),
                    new Claim("UserName", nguoiDung.UserName),
                    new Claim("Id", nguoiDung.Id.ToString()),

                    //Roles
                    new Claim("TokenId",Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes)
                , SecurityAlgorithms.HmacSha512Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            return jwtTokenHandler.WriteToken(token); 
        }
    }
}
