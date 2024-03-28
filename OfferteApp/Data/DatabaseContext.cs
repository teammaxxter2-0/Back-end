using Microsoft.EntityFrameworkCore;
using OfferteApp.Models;

namespace OfferteApp.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options) {}

    private DbSet<Quotation> Quotations { get; set; } = null!;
}