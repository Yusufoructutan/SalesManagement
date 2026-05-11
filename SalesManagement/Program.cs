using Microsoft.EntityFrameworkCore;
using SalesManagement.Data;
using SalesManagement.Repositories.Implementations;
using SalesManagement.Repositories.Interfaces;
using SalesManagement.Services.Implementations;
using SalesManagement.Services.Interfaces;

// Uygulama builder'ı oluşturulur 
var builder = WebApplication.CreateBuilder(args);

// Entity Framework Core SQL Server bağlantısı; appsettings.json içindeki "DefaultConnection" kullanılır
builder.Services.AddDbContext<TestDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Repository kayıtları 
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
builder.Services.AddScoped<ISalesRepository, SalesRepository>();

// Service kayıtları 
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISalesService, SalesService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped<IReportService, ReportService>();

// MVC controllerları ve View desteği eklenir; JSON property isimleri model ile birebir korunur 
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
    options.JsonSerializerOptions.PropertyNamingPolicy = null);



// Yapılandırılan servislerle uygulama  inşa edilir
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); 
    
    app.UseHsts();
}

app.UseHttpsRedirection(); 
app.UseStaticFiles(); 

app.UseRouting();

app.UseAuthorization(); 

// Varsayılan controller route'u: Home/Index üzerinden başlar, opsiyonel id parametresi alır
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); // Uygulama başlatılır ve gelen istekleri dinlemeye başlar
