using Microsoft.AspNetCore.Mvc;
using OfferteApp.Data;
using OfferteApp.Models;
using OfferteApp.Services;

namespace OfferteApp.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountService _service;

    public AccountController(DatabaseContext context)
    {
        _service = new AccountService(context);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Account>>> GetAllAccounts() => await _service.GetAllAccounts();

    [HttpGet("{id}")]
    public async Task<ActionResult<Account>> GetAccountById(int id) => await _service.GetAccountById(id);

    [HttpPost]
    public async Task<ActionResult<Account>> AddAccount(Account newAccount) => await _service.AddAccount(newAccount);

    [HttpPost("login")]
    public async Task<ActionResult<Account>> Login(LoginModel loginModel)
    {
        // Implement authentication logic here
        // Example:
        var account = await _service.Authenticate(loginModel.Username, loginModel.Password);
        if (account == null)
        {
            return Unauthorized();
        }
        return Ok(account);
    }
}
