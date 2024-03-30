using Microsoft.AspNetCore.Mvc;
using OfferteApp.Data;
using OfferteApp.Models;

namespace OfferteApp.Services;

public class QuotationService(DatabaseContext _context)
{
    public IActionResult Get()
    {
        return new OkObjectResult(_context.Quotations);
    }

    public bool Create(Quotation quote)
    {
        _context.Quotations.Add(quote);
        return _context.SaveChanges() > 0;
    }

    public bool Edit(Quotation quote)
    {
        _context.Quotations.Update(quote);
        return _context.SaveChanges() > 0;
    }

    public bool Delete(int id)
    {
        var quote = _context.Quotations.FirstOrDefault(q => q.Id == id);
        if (quote == null) return false;
        _context.Quotations.Remove(quote);
        return _context.SaveChanges() > 0;
    }
}