using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OfferteApp.Data;
using OfferteApp.Models;
using OfferteApp.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static Account loginAccount = new();
        private readonly DatabaseContext _dataContext = new();
        private readonly IConfiguration _configuration;
        private readonly AccountService _userService;

        public AuthController(IConfiguration configuration, AccountService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<Account>> AddAccount(Account newAccount) => await _userService.AddAccount(newAccount);

        [HttpPost("login")]
        public async Task<ActionResult<Account>> Login(LoginModel request)
        {
            // Implement authentication logic here
            if (_dataContext == null)
            {
                return BadRequest("accountdb is null");
            }
            string encryptedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var correspondingAccount = await _userService.Authenticate(request.Username, request.Password);
            if (correspondingAccount == null)
            {
                return NotFound("Invalid credentials");
            }
            if (correspondingAccount.Username == request.Username && BCrypt.Net.BCrypt.Verify(correspondingAccount.Password, encryptedPassword))
            {
                loginAccount.AccountId = correspondingAccount!.AccountId;
                return Ok(loginAccount);
            }
            // Example:

            return NotFound("Invalid credentials");
        }


    }
}