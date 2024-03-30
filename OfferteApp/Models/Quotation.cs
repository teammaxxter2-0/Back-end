namespace OfferteApp.Models;

public class Quotation
{
    public int Id { get; set; }
    public List<Product> Products { get; set; }
    public string Material { get; set; }
    public DateTime Creation { get; set; }
}