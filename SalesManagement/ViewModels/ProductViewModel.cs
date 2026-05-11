namespace SalesManagement.ViewModels
{
    /// <summary>
    /// Ürün listeleme ekranlarında kullanılan, kategori adını da içeren sunum amaçlı veri modeli.
    /// </summary>
    public class ProductViewModel
    {
        /// <summary>Ürünün veritabanındaki birincil anahtarı.</summary>
        public int Id { get; set; }

        /// <summary>Ürün adı.</summary>
        public string? Name { get; set; }

        /// <summary>Ürün görselinin yolu/URL'si.</summary>
        public string? ImageSrc { get; set; }

        /// <summary>Ürünün liste (satış) fiyatı.</summary>
        public double? Salesprice { get; set; }

        /// <summary>Ürünün ait olduğu kategorinin görünen adı.</summary>
        public string? CategoryName { get; set; }

        /// <summary>Ürünün ait olduğu kategorinin ID'si.</summary>
        public int? CategoryId { get; set; }
    }
}
