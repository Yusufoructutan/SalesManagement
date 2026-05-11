using SalesManagement.Models;

namespace SalesManagement.Repositories.Interfaces
{

    public interface IProductRepository
    {
    
        // Tüm ürünleri kategoriyle birlikte getirir
       public IEnumerable<Product> GetAll();


        // ID'ye göre ürün getirir
        public Product? GetById(int id);


        // Yeni ürün ekler
        public void Add(Product product);


        // Ürün günceller
        public void Update(Product product);
    }
}
