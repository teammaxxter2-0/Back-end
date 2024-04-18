using OfferteApp.Models;
using OfferteApp.Data;

namespace Backend.Data;
public static class DBSeeding
{

    public static void Seed()
    {
        Account acc1 = new() { Name = "Test", Password = "123" };
        List<Account> accounts = new() { acc1 };

        var db = new DatabaseContext();

        foreach (Account acc in accounts)
        {
            db.Add(acc);
        }
    }

}