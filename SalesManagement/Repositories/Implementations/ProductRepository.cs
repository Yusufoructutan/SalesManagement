using Microsoft.EntityFrameworkCore;
using SalesManagement.Data;
using SalesManagement.Models;
using SalesManagement.Repositories.Interfaces;

namespace SalesManagement.Repositories.Implementations
{
    /// <summary>
    /// IProductRepository arayüzünün EF Core ile uygulaması; ürünler üzerinde CRUD işlemlerini gerçekleştirir.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly TestDbContext _context;

        /// <summary>
        /// Veritabanı context'i bağımlılığını DI üzerinden alır.
        /// </summary>
        public ProductRepository(TestDbContext context)
        {
            _context = context;
        }

    
        // Tüm ürünleri kategori bilgisiyle birlikte getirir
        public IEnumerable<Product> GetAll()
        {
            return _context.Products
                .Include(p => p.Category) // Kategori eager-load edilir
                .ToList();
        }


        // ID'ye göre  ürün getirir
        public Product? GetById(int id)
        {
            return _context.Products.Find(id);
        }


        // Yeni ürün ekler ve değişiklikleri kaydeder
        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        // Mevcut ürünü günceller ve değişiklikleri kaydeder
        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
    }
}
