using CodingExam.Domain.Interfaces;
using CodingExam.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CodingExam.Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        private readonly IConfiguration _config;

        public AuthService(IAuthRepository authRepository, IConfiguration config)
        {
            _authRepository = authRepository;

            _config = config;
        }

        public async Task<string> GenerateAccessToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(expires: DateTime.Now.AddMinutes(15),
                claims: new List<Claim> { new Claim(ClaimTypes.NameIdentifier, username) },
              signingCredentials: credentials);

            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public async Task<string> GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomNumber);

            return await Task.FromResult(Convert.ToBase64String(randomNumber));
        }

        public async Task<bool> ValidateLogin(User user)
        {
            return await _authRepository.ValidateLogin(user);
        }
    }
}
