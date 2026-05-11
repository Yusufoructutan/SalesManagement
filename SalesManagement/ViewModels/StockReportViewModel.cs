namespace SalesManagement.ViewModels
{
    /// <summary>
    /// Stok raporu ekranında kullanılan; ürün ve kategori bazında toplam stok bilgisini
    /// içeren sunum amaçlı veri modeli.
    /// </summary>
    public class StockReportViewModel
    {
        /// <summary>Ürünün ID'si.</summary>
        public int ProductId { get; set; }

        /// <summary>Ürünün adı.</summary>
        public string? ProductName { get; set; }

        /// <summary>Ürünün ait olduğu kategorinin adı.</summary>
        public string? CategoryName { get; set; }

        /// <summary>Ürün için Stock tablosundaki tüm hareketlerin toplamı (alımlar - satışlar).</summary>
        public double? TotalStock { get; set; }
    }
}
