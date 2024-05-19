using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OfferteApp.Data;
using OfferteApp.Models;

namespace OfferteApp.Services;

public class QuotationService(DatabaseContext _context)
{
    public async Task<ActionResult<IEnumerable<Quotation>>> Get()
    {
        return new OkObjectResult(_context.Quotations);
    }

    public async Task<ActionResult<Quotation>> Create(Quotation quote)
    {
        _context.Quotations.Add(quote);
        await _context.SaveChangesAsync();
        return quote;
    }

    public async Task<ActionResult<Quotation>> Edit(Quotation quote)
    {
        _context.Quotations.Update(quote);
        await _context.SaveChangesAsync();
        return quote;
    }

    public async Task<ActionResult> Delete(int id)
    {
        var quote = await _context.Quotations.FindAsync(id);
        if (quote == null)
        {
            return NotFoundResult();
        }
        _context.Quotations.Remove(quote);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}