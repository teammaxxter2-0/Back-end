using Microsoft.AspNetCore.Mvc;
using OfferteApp.Data;
using OfferteApp.Models;
using OfferteApp.Services;

namespace OfferteApp.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController(DatabaseContext context) : ControllerBase
{
    private readonly AccountService _service = new(context);

    [HttpGet] public async Task<ActionResult<IEnumerable<Account>>> GetAllAccounts() => await _service.GetAllAccounts();
    [HttpGet("{id}")] public async Task<ActionResult<Account>> GetAccountById(int id) => await _service.GetAccountById(id);
    [HttpPost] public async Task<ActionResult<Account>> AddAccount(Account newAccount) => await _service.AddAccount(newAccount);

}