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
    /// Satış işlemlerinin (listeleme, oluşturma, fiyat sorgulama) yönetildiği controller.
    /// </summary>
    public class SalesController : Controller
    {
        private readonly ISalesService _salesService;
        private readonly TestDbContext _context;

        /// <summary>
        /// Bağımlılıkları (satış servisi ve veritabanı context'i) DI üzerinden alır.
        /// </summary>
        public SalesController(ISalesService salesService, TestDbContext context)
        {
            _salesService = salesService;
            _context = context;
        }


        // Satış listesi sayfası
        public IActionResult Index()
        {
            return View();
        }


        // DataGrid için JSON veri kaynağı
        public IActionResult GetSales(DataSourceLoadOptions loadOptions)
        {
            var sales = _salesService.GetAll();
            return Json(DataSourceLoader.Load(sales, loadOptions));
        }


        // Yeni satış formu
        public IActionResult Create()
        {
       
            ViewBag.Customers = new SelectList(
                _context.Customers.Select(c => new {
                    c.Id,
                    FullName = c.Customertitle + " " + c.Customernumber 
                }),
                "Id", "FullName"
            );

            // Ürün listesi
            ViewBag.Products = new SelectList(_context.Products, "Id", "Name");

            return View();
        }


        // Ürün seçilince liste fiyatını döner
        [HttpGet]
        public IActionResult GetProductPrice(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product == null) return NotFound(); 
            return Json(new { listPrice = product.Salesprice });
        }


        // Yeni satış kaydet
        [HttpPost]
        public IActionResult Create(Sale sale)
        {
            var (success, message) = _salesService.Add(sale); 

            if (success)
                return RedirectToAction(nameof(Index));

            ViewBag.ErrorMessage = message; // Kullanıcıya gösterilecek hata mesajı
            ViewBag.Customers = new SelectList(
                _context.Customers.Select(c => new {
                    c.Id,
                    FullName = c.Customertitle + " " + c.Customernumber
                }),
                "Id", "FullName"
            );
            ViewBag.Products = new SelectList(_context.Products, "Id", "Name");
            return View(sale);
        }
    }
}
