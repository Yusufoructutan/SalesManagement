using SalesManagement.Data;
using SalesManagement.Models;
using SalesManagement.Repositories.Interfaces;
using SalesManagement.Services.Interfaces;
using SalesManagement.ViewModels;

namespace SalesManagement.Services.Implementations
{
    /// <summary>
    /// ISalesService arayüzünün uygulaması; satış kaydı, stok kontrolü, iskonto hesaplama
    /// ve stok hareketi oluşturma iş mantığını yürütür.
    /// </summary>
    public class SalesService : ISalesService
    {
        private readonly ISalesRepository _salesRepository;
        private readonly TestDbContext _context;

        /// <summary>
        /// Satış repository ve veritabanı context'i bağımlılıklarını DI üzerinden alır.
        /// </summary>
        public SalesService(ISalesRepository salesRepository, TestDbContext context)
        {
            _salesRepository = salesRepository;
            _context = context;
        }

        // Satışları ViewModel'e dönüştürerek döner
        public IEnumerable<SalesViewModel> GetAll()
        {
            return _salesRepository.GetAll().Select(s => new SalesViewModel
            {
                Id = s.Id,
                CustomerName = s.Customer?.Customertitle + " " + s.Customer?.Customernumber, // Müşteri adı + numara birleşimi
                ProductName = s.Product?.Name,
                Quantity = s.Quantity,
                SalesPrice = s.Salesprice,
                ListPrice = s.Listprice,
                DiscountRate = s.Discountrate,
                Amount = s.Amount,
                Date = s.Date
            });
        }


        // Satış ekler, stok kontrolü yapar, Stock tablosuna yazar
        public (bool success, string message) Add(Sale sale)
        {
            // Mevcut stok miktarını hesapla
            var mevcutStok = _context.Stocks
                .Where(s => s.ProductId == sale.ProductId)
                .Sum(s => s.Quantity); // Pozitif (alım) ve negatif (satış) hareketlerin toplamı

            // Stok yeterliliği kontrolü
            if (mevcutStok < sale.Quantity)
                return (false, $"Yetersiz stok! Mevcut stok: {mevcutStok}"); // Stok yetersizse satış reddedilir

            // İskonto oranını hesapıla
            if (sale.Listprice > 0)
                sale.Discountrate = (sale.Listprice - sale.Salesprice) / sale.Listprice * 100;

            // Toplam tutarı hesapla
            sale.Amount = sale.Quantity * sale.Salesprice; 
            sale.Date = DateTime.Now;

            // Satışı kaydet
            _salesRepository.Add(sale);

            // Stock tablosuna negatif miktar olarak yaz (stok düşümü)
            var stokHareketi = new Stock
            {
                ProductId = sale.ProductId,
                Quantity = -sale.Quantity, // Satış = stok azalır
                Date = DateTime.Now
            };
            _context.Stocks.Add(stokHareketi);
            _context.SaveChanges();

            return (true, "Satış başarıyla kaydedildi.");
        }
    }
}
