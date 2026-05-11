using Microsoft.EntityFrameworkCore;
using SalesManagement.Data;
using SalesManagement.Models;
using SalesManagement.Repositories.Interfaces;

namespace SalesManagement.Repositories.Implementations
{
    public class SalesRepository : ISalesRepository
    {
        private readonly TestDbContext _context;

        /// <summary>
        /// Veritabanı context'i bağımlılığını DI üzerinden alır.
        /// </summary>
        public SalesRepository(TestDbContext context)
        {
            _context = context;
        }

        // Tüm satışları müşteri ve ürün bilgisiyle getirir
        public IEnumerable<Sale> GetAll()
        {
            return _context.Sales
                .Include(s => s.Customer) 
                .Include(s => s.Product) 
                .ToList();
        }


        // Yeni satış kaydeder
        public void Add(Sale sale)
        {
            _context.Sales.Add(sale);
            _context.SaveChanges();
        }
    }
}
