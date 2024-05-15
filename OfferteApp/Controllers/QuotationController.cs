using Microsoft.AspNetCore.Mvc;
using OfferteApp.Data;
using OfferteApp.Models;
using OfferteApp.Services;

namespace OfferteApp.Controllers;

[ApiController]
[Route("[controller]")]
public class QuotationController(DatabaseContext context) : ControllerBase
{
    private readonly QuotationService _service = new(context);

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Quotation>>> Get()
    {
        return await _service.Get();
    }


    [HttpPost]
    public async Task<ActionResult<Quotation>> Create([FromBody] Quotation quote)
    {
        return await _service.Create(quote);
    }

    [HttpPut]
    public async Task<ActionResult<Quotation>> Edit([FromBody] Quotation quote)
    {
        return await _service.Edit(quote);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        return await _service.Delete(id);
    }
}
