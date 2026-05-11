# SalesManagement

ASP.NET Core MVC (.NET 8) ile geliştirilmiş satış, satın alım ve stok yönetimi uygulaması.

## Teknolojiler
- ASP.NET Core MVC (.NET 8)
- Entity Framework Core 8 (Database First)
- SQL Server
- DevExtreme 25.2 (DataGrid)
- Bootstrap 5

## Mimari
Proje **katmanlı mimari** prensibiyle geliştirilmiştir.
Controller → Service → Repository → DbContext → Database


- **Controller** — HTTP isteklerini karşılar
- **Service** — İş kuralları (stok kontrolü, iskonto hesaplama)
- **Repository** — Veritabanı sorguları
- **ViewModel** — View'a özel veri modelleri

## Özellikler
- Ürün ekleme ve güncelleme
- Satış ekranı (otomatik iskonto hesaplama, stok kontrolü)
- Satın alım ekranı (otomatik stok artışı)
- Kategoriye göre satış raporu (tarih filtreli)
- Stok raporu

## Kurulum
1. `db.sql` dosyasını SQL Server'da çalıştır
2. `appsettings.json` içindeki connection string'i güncelle
3. Projeyi çalıştır