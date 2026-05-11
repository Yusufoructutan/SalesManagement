using SalesManagement.Data;
using SalesManagement.Models;
using SalesManagement.Repositories.Interfaces;
using SalesManagement.Services.Interfaces;
using SalesManagement.ViewModels;

namespace SalesManagement.Services.Implementations
{
    /// <summary>
    /// IPurchaseService arayüzünün uygulaması; satın alım kaydı ve Stock tablosuna pozitif hareket
    /// yazma iş mantığını yürütür.
    /// </summary>
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly TestDbContext _context;

        /// <summary>
        /// Satın alım repository ve veritabanı context'i bağımlılıklarını DI üzerinden alır.
        /// </summary>
        public PurchaseService(IPurchaseRepository purchaseRepository, TestDbContext context)
        {
            _purchaseRepository = purchaseRepository;
            _context = context;
        }

        // Satın alımları ViewModel'e dönüştürerek döner
        public IEnumerable<PurchaseViewModel> GetAll()
        {
            return _purchaseRepository.GetAll().Select(p => new PurchaseViewModel
            {
                Id = p.Id,
                CustomerName = p.Customer?.Customertitle + " " + p.Customer?.Customernumber, // Müşteri adı + numara birleşimi
                ProductName = p.Product?.Name,
                Quantity = p.Quantity,
                Price = p.Price,
                Amount = p.Amount,
                Date = p.Date
            });
        }


        // Satın alım kaydeder ve Stock tablosuna pozitif miktar yazar
        public void Add(Purchase purchase)
        {
            // Toplam tutarı hesapla
            purchase.Amount = purchase.Quantity * purchase.Price; // Miktar x birim alış fiyatı
            purchase.Date = DateTime.Now;

            // Satın alımı kaydet
            _purchaseRepository.Add(purchase);

            // Stock tablosuna pozitif miktar yaz (stok artışı)
            var stokHareketi = new Stock
            {
                ProductId = purchase.ProductId,
                Quantity = purchase.Quantity, // Alım = stok artar
                Date = DateTime.Now
            };
            _context.Stocks.Add(stokHareketi);
            _context.SaveChanges();
        }
    }
}
