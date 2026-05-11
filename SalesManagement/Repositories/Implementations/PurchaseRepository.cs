using Microsoft.EntityFrameworkCore;
using SalesManagement.Data;
using SalesManagement.Models;
using SalesManagement.Repositories.Interfaces;

namespace SalesManagement.Repositories.Implementations
{

    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly TestDbContext _context;

        /// <summary>
        /// Veritabanı context'i bağımlılığını DI üzerinden alır.
        /// </summary>
        public PurchaseRepository(TestDbContext context)
        {
            _context = context;
        }

        // Tüm satın alımları müşteri ve ürün bilgisiyle getirir
        public IEnumerable<Purchase> GetAll()
        {
            return _context.Purchases
                .Include(p => p.Customer) 
                .Include(p => p.Product) 
                .ToList();
        }

        // Yeni satın alım ekler
        public void Add(Purchase purchase)
        {
            _context.Purchases.Add(purchase);
            _context.SaveChanges();
        }
    }
}
