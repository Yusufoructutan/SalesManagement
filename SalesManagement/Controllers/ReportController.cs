using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using SalesManagement.Services.Interfaces;

namespace SalesManagement.Controllers
{
    /// <summary>
    /// Satış ve stok raporlama sayfalarını ve veri kaynaklarını yöneten controller.
    /// </summary>
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;

        /// <summary>
        /// Rapor servisini DI üzerinden alır.
        /// </summary>
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

 
        // Kategoriye göre satış raporu sayfası
        public IActionResult SalesReport()
        {
            return View();
        }


        // DataGrid için JSON veri kaynağı - tarih filtreli
        public IActionResult GetSalesReport(DataSourceLoadOptions loadOptions, DateTime? startDate, DateTime? endDate)
        {
            var report = _reportService.GetSalesReport(startDate, endDate); 
            return Json(DataSourceLoader.Load(report, loadOptions));
        }


        // Stok raporu sayfası
        public IActionResult StockReport()
        {
            return View();
        }

 
        // DataGrid için JSON veri kaynağı
        public IActionResult GetStockReport(DataSourceLoadOptions loadOptions)
        {
            var report = _reportService.GetStockReport();
            return Json(DataSourceLoader.Load(report, loadOptions));
        }
    }
}
