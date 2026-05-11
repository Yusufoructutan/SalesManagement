using Microsoft.EntityFrameworkCore;
using SalesManagement.Data;
using SalesManagement.Services.Interfaces;
using SalesManagement.ViewModels;

namespace SalesManagement.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly TestDbContext _context;

        /// <summary>
        /// Veritabanı context'i bağımlılığını DI üzerinden alır.
        /// </summary>
        public ReportService(TestDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Satış raporunu üretir; tarih filtresi uygular, ürün bazında toplam satış miktarını hesaplar
        /// ve sonucu en çok satan ürün başta olacak şekilde sıralayarak döner.
        /// </summary>
        public IEnumerable<SalesReportViewModel> GetSalesReport(DateTime? startDate, DateTime? endDate)
        {
            // Satışları ürün, kategori ve müşteri bilgileriyle birlikte getir
            var query = _context.Sales
                .Include(s => s.Product)
                .ThenInclude(p => p!.Category) 
                .Include(s => s.Customer)
                .AsQueryable();//Sorguyu IQueryable olarak tutarak tarih filtrelerini dinamik olarak ekleyelim

            // Başlangıç tarihi filtresi 
            if (startDate.HasValue)
                query = query.Where(s => s.Date.HasValue && s.Date.Value.Date >= startDate.Value.Date);

            // Bitiş tarihi filtresi 
            if (endDate.HasValue)
                query = query.Where(s => s.Date.HasValue && s.Date.Value.Date <= endDate.Value.Date);

            var list = query.ToList();

            // Her ürün için toplam satış miktarını hesapla
            var totalQuantities = list
                .GroupBy(s => s.ProductId)
                .ToDictionary(g => g.Key, g => g.Sum(s => s.Quantity));

            return list.Select(s => new SalesReportViewModel
            {
                Id = s.Id,
                ProductId = s.ProductId ?? 0,
                ProductName = s.Product?.Name,
                CategoryId = s.Product?.CategoryId ?? 0,
                CategoryName = s.Product?.Category?.Name,
                CustomerId = s.CustomerId ?? 0,
                CustomerName = s.Customer?.Customertitle + " " + s.Customer?.Customernumber,
                Quantity = s.Quantity,
                SalesPrice = s.Salesprice,
                DiscountRate = s.Discountrate,
                Date = s.Date,
                TotalQuantity = totalQuantities.GetValueOrDefault(s.ProductId)
            }).OrderByDescending(s => s.TotalQuantity); // En çok satan ürün üstte
        }

        /// <summary>
        /// Stok raporunu üretir; ürün bazında Stock hareketlerini gruplayıp toplam stoku hesaplar.
        /// </summary>
        public IEnumerable<StockReportViewModel> GetStockReport()
        {
            return _context.Stocks
                .Include(s => s.Product)
                .ThenInclude(p => p!.Category)
                .GroupBy(s => new {
                    s.ProductId,
                    s.Product!.Name,
                    CategoryName = s.Product.Category!.Name
                }) // Ürün ve kategorisine göre gruplama
                .Select(g => new StockReportViewModel
                {
                    ProductId = g.Key.ProductId ?? 0,
                    ProductName = g.Key.Name,
                    CategoryName = g.Key.CategoryName,
                    TotalStock = g.Sum(s => s.Quantity) // Pozitif (alım) ve negatif (satış) hareketlerin toplamı
                })
                .ToList();
        }
    }
}