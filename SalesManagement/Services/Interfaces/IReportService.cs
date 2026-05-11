using SalesManagement.ViewModels;

namespace SalesManagement.Services.Interfaces
{
    public interface IReportService
    {
        // Kategoriye göre satış raporu - tarih filtreli
        public IEnumerable<SalesReportViewModel> GetSalesReport(DateTime? startDate, DateTime? endDate);


        // Stok raporu
        public IEnumerable<StockReportViewModel> GetStockReport();
    }
}
