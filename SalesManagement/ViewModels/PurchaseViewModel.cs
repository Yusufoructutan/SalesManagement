namespace SalesManagement.ViewModels
{
    /// <summary>
    /// Satın alım listeleme ekranlarında kullanılan; müşteri ve ürün adlarını birleştirilmiş şekilde
    /// içeren sunum amaçlı veri modeli.
    /// </summary>
    public class PurchaseViewModel
    {
        /// <summary>Satın alımın veritabanındaki birincil anahtarı.</summary>
        public int Id { get; set; }

        /// <summary>Tedarikçi/müşterinin görünen adı (ünvan + numara birleşimi).</summary>
        public string? CustomerName { get; set; }

        /// <summary>Satın alınan ürünün adı.</summary>
        public string? ProductName { get; set; }

        /// <summary>Satın alınan miktar.</summary>
        public double? Quantity { get; set; }

        /// <summary>Birim alış fiyatı.</summary>
        public double? Price { get; set; }

        /// <summary>Satın alım toplam tutarı (Miktar x Birim Fiyat).</summary>
        public double? Amount { get; set; }

        /// <summary>Satın alımın gerçekleştirildiği tarih.</summary>
        public DateTime? Date { get; set; }
    }
}
