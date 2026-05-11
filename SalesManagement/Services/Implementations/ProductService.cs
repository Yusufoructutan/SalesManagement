using SalesManagement.Models;
using SalesManagement.Repositories.Interfaces;
using SalesManagement.Services.Interfaces;
using SalesManagement.ViewModels;

namespace SalesManagement.Services.Implementations
{

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Ürün repository bağımlılığını DI üzerinden alır.
        /// </summary>
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        // Ürünleri ViewModel'e dönüştürerek döner
        public IEnumerable<ProductViewModel> GetAll()
        {
            return _productRepository.GetAll().Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                ImageSrc = p.ImageSrc,
                Salesprice = p.Salesprice,
                CategoryName = p.Category?.Name, 
                CategoryId = p.CategoryId
            });
        }

        /// <summary>
        /// ID'ye göre ürünü repository üzerinden döner.
        /// </summary>
        public Product? GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        /// <summary>
        /// Yeni ürünü repository üzerinden veritabanına ekler.
        /// </summary>
        public void Add(Product product)
        {
            _productRepository.Add(product);
        }

        /// <summary>
        /// Mevcut ürünün bilgilerini repository üzerinden günceller.
        /// </summary>
        public void Update(Product product)
        {
            _productRepository.Update(product);
        }
    }
}
