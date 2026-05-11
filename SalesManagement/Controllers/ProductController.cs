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
    /// Ürün yönetimi (listeleme, ekleme, güncelleme) işlemlerini yöneten controller.
    /// </summary>
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly TestDbContext _context;

        /// <summary>
        /// Bağımlılıkları (ürün servisi ve veritabanı context'i) DI üzerinden alır.
        /// </summary>
        public ProductController(IProductService productService, TestDbContext context)
        {
            _productService = productService;
            _context = context;
        }

  
        // Ürün listesi sayfası
        public IActionResult Index()
        {
            return View();
        }

        // DataGrid için JSON veri kaynağı
        public IActionResult GetProducts(DataSourceLoadOptions loadOptions)
        {
            var products = _productService.GetAll();
            return Json(DataSourceLoader.Load(products, loadOptions));
        }


        // Yeni ürün formu
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name"); 
            return View();
        }

  
        // Yeni ürün kaydet
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid) // Model doğrulamasından geçtiyse kaydet
            {
                _productService.Add(product);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name"); // Hata durumunda kategori listesini tekrar yükle
            return View(product);
        }


        // Ürün düzenleme formu
        public IActionResult Edit(int id)
        {
            var product = _productService.GetById(id);
            if (product == null) return NotFound(); // Ürün bulunamazsa 404 döndür
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }


        // Ürün güncelle
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid) // Model doğrulamasından geçtiyse güncelle
            {
                _productService.Update(product);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }
    }
}
