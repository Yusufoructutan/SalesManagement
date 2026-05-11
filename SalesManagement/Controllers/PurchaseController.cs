using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalesManagement.Data;
using SalesManagement.Models;
using SalesManagement.Services.Interfaces;

namespace SalesManagement.Controllers
{
    /// <summary>
    /// Satın alım işlemlerinin (listeleme, oluşturma) yönetildiği controller.
    /// </summary>
    public class PurchaseController : Controller
    {
        private readonly IPurchaseService _purchaseService;
        private readonly TestDbContext _context;

        /// <summary>
        /// Bağımlılıkları (satın alım servisi ve veritabanı context'i) DI üzerinden alır.
        /// </summary>
        public PurchaseController(IPurchaseService purchaseService, TestDbContext context)
        {
            _purchaseService = purchaseService;
            _context = context;
        }

        // Satın alım listesi sayfası
        public IActionResult Index()
        {
            return View();
        }

        // DataGrid için JSON veri kaynağı
        public IActionResult GetPurchases(DataSourceLoadOptions loadOptions)
        {
            var purchases = _purchaseService.GetAll();
            return Json(DataSourceLoader.Load(purchases, loadOptions));
        }


        // Yeni satın alım formu
        public IActionResult Create()
        {
            // Müşteri listesi: Ad ve Numara birlikte gösterilir
            ViewBag.Customers = new SelectList(
                _context.Customers.Select(c => new {
                    c.Id,
                    FullName = c.Customertitle + " " + c.Customernumber // Müşteri adı + numarası birleşimi
                }),
                "Id", "FullName"
            );

            // Ürün listesi
            ViewBag.Products = new SelectList(_context.Products, "Id", "Name");

            return View();
        }


        // Yeni satın alım kaydet
        [HttpPost]
        public IActionResult Create(Purchase purchase)
        {
            _purchaseService.Add(purchase); // Stok artışı servis katmanında yapılır
            return RedirectToAction(nameof(Index));
        }
    }
}
