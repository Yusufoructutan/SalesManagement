namespace SalesManagement.ViewModels
{
    /// <summary>
    /// Satış raporu ekranında kullanılan; ürün, kategori ve müşteri bazlı satış detaylarını
    /// ve toplam satış miktarını içeren sunum amaçlı veri modeli.
    /// </summary>
    public class SalesReportViewModel
    {
        public int Id { get; set; }  // ← Sales tablosunun ID'si, benzersiz

        /// <summary>Ürünün ID'si.</summary>
        public int ProductId { get; set; }

        /// <summary>Ürünün adı.</summary>
        public string? ProductName { get; set; }

        /// <summary>Kategorinin ID'si.</summary>
        public int CategoryId { get; set; }

        /// <summary>Kategorinin adı.</summary>
        public string? CategoryName { get; set; }

        /// <summary>Müşterinin ID'si.</summary>
        public int CustomerId { get; set; }

        /// <summary>Müşterinin görünen adı (ünvan + numara birleşimi).</summary>
        public string? CustomerName { get; set; }

        /// <summary>İlgili satış kaydının miktarı.</summary>
        public double? Quantity { get; set; }

        /// <summary>İlgili satış kaydının birim satış fiyatı.</summary>
        public double? SalesPrice { get; set; }

        /// <summary>İlgili satış kaydındaki iskonto oranı (%).</summary>
        public double? DiscountRate { get; set; }

        /// <summary>Satışın gerçekleştirildiği tarih.</summary>
        public DateTime? Date { get; set; }

        /// <summary>Aynı ürün için filtrelenmiş aralıkta yapılan tüm satışların toplam miktarı.</summary>
        public double? TotalQuantity { get; set; }
    }
}
