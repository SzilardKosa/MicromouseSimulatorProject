using AspNetCore.Identity.Mongo.Model;
using MicromouseSimulatorBackend.BLL.Config;
using MicromouseSimulatorBackend.BLL.Models;
using MicromouseSimulatorBackend.BLL.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MicromouseSimulatorBackend.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<MongoUser> _userManager;
        private readonly SignInManager<MongoUser> _signInManager;
        private readonly IJwtSettings _settings;

        public AuthService(UserManager<MongoUser> userManager, SignInManager<MongoUser> signInManager, IJwtSettings settings)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._settings = settings;
        }
        
        public async Task Register(NewUser newUser)
        {
            var user = new MongoUser { Email = newUser.Email, UserName = newUser.Email };
            var result = await _userManager.CreateAsync(user, newUser.Password);
            if (!result.Succeeded)
            {
                throw new Exception("User couldn't be created! " + result.Errors.First().Description);
            }
        }

        public async Task<AuthToken> Login(Login login)
        {
            var user = await _userManager.FindByNameAsync(login.Email);
            if (user == null)
                throw new Exception("User doesn't exist!");
            var isPasswordValid = await _signInManager.UserManager.CheckPasswordAsync(user, login.Password);
            if (!isPasswordValid)
                throw new Exception("Invalid password!");

            return generateJWTToken(user);
        }

        private const int EXPIRY_DURATION_DAYS = 7;

        private AuthToken generateJWTToken(MongoUser user)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _settings.Issuer,
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Email.ToString()),
                }),
                Expires = DateTime.Now.AddDays(EXPIRY_DURATION_DAYS),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key)),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            return new AuthToken
            {
                Token = token,
            };
        }

    }
}
