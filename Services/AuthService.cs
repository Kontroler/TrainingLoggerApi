using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TrainingLogger.API.Data;
using TrainingLogger.API.Models;
using TrainingLogger.Exceptions;

namespace TrainingLogger.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthService(IAuthRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;   
        }
        public async Task<string> Login(string username, string password)
        {
            var userFromRepo = await _repo.Login(username, password);

            if (userFromRepo == null) return null;

            return GenerateToken(userFromRepo);
        }

         private string GenerateToken(User user) 
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);        
            return tokenHandler.WriteToken(token);
        }

        public async Task<User> Register(string username, string password)
        {
            username = username.ToLower();

            if (await _repo.UserExists(username)) 
                throw new UsernameAlreadyExistsException("Username already exists.");
            

            var userToCreate = new User
            {
                Username = username
            };
            
            var createdUser = await _repo.Register(userToCreate, password);
            return createdUser;
        }

    }
}