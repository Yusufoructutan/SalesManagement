namespace SalesManagement.ViewModels
{
    /// <summary>
    /// Satış listeleme ekranlarında kullanılan; müşteri ve ürün adlarını birleştirilmiş şekilde
    /// içeren sunum amaçlı veri modeli.
    /// </summary>
    public class SalesViewModel
    {
        /// <summary>Satışın veritabanındaki birincil anahtarı.</summary>
        public int Id { get; set; }

        /// <summary>Müşterinin görünen adı (ünvan + numara birleşimi).</summary>
        public string? CustomerName { get; set; }

        /// <summary>Satılan ürünün adı.</summary>
        public string? ProductName { get; set; }

        /// <summary>Satılan miktar.</summary>
        public double? Quantity { get; set; }

        /// <summary>Birim satış fiyatı.</summary>
        public double? SalesPrice { get; set; }

        /// <summary>Ürünün liste (etiket) fiyatı.</summary>
        public double? ListPrice { get; set; }

        /// <summary>Liste fiyatına göre uygulanan iskonto oranı (%).</summary>
        public double? DiscountRate { get; set; }

        /// <summary>Satış toplam tutarı (Miktar x Birim Fiyat).</summary>
        public double? Amount { get; set; }

        /// <summary>Satışın gerçekleştirildiği tarih.</summary>
        public DateTime? Date { get; set; }
    }
}
