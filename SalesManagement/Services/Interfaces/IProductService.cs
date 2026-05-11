using SalesManagement.Models;
using SalesManagement.ViewModels;

namespace SalesManagement.Services.Interfaces
{
    public interface IProductService
    {

        // Tüm ürünleri ViewModel listesi olarak döner
         public IEnumerable<ProductViewModel> GetAll();

        // ID'ye göre ürün döner
       public Product? GetById(int id);


        // Yeni ürün ekler
        public void Add(Product product);

  
        // Ürün günceller
        public void Update(Product product);
    }
}
