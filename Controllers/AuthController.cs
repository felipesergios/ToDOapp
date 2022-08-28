using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using api_app.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api_app.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _sigInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

        public AuthController(SignInManager<IdentityUser> sigInManager, UserManager<IdentityUser> userManager,IOptions<AppSettings> appSettings)
        {
            _sigInManager = sigInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost("nova-conta")]
        public async Task<ActionResult> Registrar(RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            await _sigInManager.SignInAsync(user, false);
            return Ok(await GerarJwt(registerUser.Email));
        }
        [HttpPost("entrar")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
            var result = await _sigInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false , true);
            if (result.Succeeded) return Ok(await GerarJwt(loginUser.Email));
            return BadRequest("Usuario ou senha invalidos");
        }

        private async Task<string> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var anotherKey = Encoding.ASCII.GetBytes(_appSettings.Secret);

            Console.WriteLine(anotherKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(anotherKey), SecurityAlgorithms.HmacSha256Signature),
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

        }


    }
}

