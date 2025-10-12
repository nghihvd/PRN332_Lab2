using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PRN232.Lab2.CoffeeStore.Repositories.Database;
using PRN232.Lab2.CoffeeStore.Repositories.Models;
using PRN232.Lab2.CoffeeStore.Services.Interfaces;
using PRN232.Lab2.CoffeeStore.Services.Models.Requests.Auth;
using PRN232.Lab2.CoffeeStore.Services.Models.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PRN232.Lab2.CoffeeStore.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly DatabaseContext _db;
        private readonly IConfiguration _config;

        public AuthService(DatabaseContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        public async Task<AuthResponseModel> RegisterAsync(RegisterRequestModel request)
        {
            if (await _db.Users.AnyAsync(u => u.Username == request.Username))
            {
                throw new InvalidOperationException("Tên người dùng đã tồn tại.");
            }
            if (await _db.Users.AnyAsync(u => u.Email == request.Email))
            {
                throw new InvalidOperationException("Email đã tồn tại.");
            }

            CreatePasswordHash(request.Password, out var hash, out var salt);
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = "User",
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };
            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return await GenerateAuthResponse(user);
        }

        public async Task<AuthResponseModel> LoginAsync(LoginRequestModel request)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null || !VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new InvalidOperationException("Tên người dùng hoặc mật khẩu không đúng.");
            }
            return await GenerateAuthResponse(user);
        }

        private async Task<AuthResponseModel> GenerateAuthResponse(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);
            var expires = DateTime.UtcNow.AddHours(8);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserId),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = expires,
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return new AuthResponseModel
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                Token = jwt,
                ExpiresAt = expires
            };
        }

        private static void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using var hmac = new HMACSHA256();
            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private static bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new HMACSHA256(storedSalt);
            var computed = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computed.SequenceEqual(storedHash);
        }
    }
}


