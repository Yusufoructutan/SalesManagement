using SalesManagement.Models;
using SalesManagement.ViewModels;

namespace SalesManagement.Services.Interfaces
{

    public interface ISalesService
    {

        // Tüm satışları ViewModel olarak döner
        IEnumerable<SalesViewModel> GetAll();

        // Satış ekler, stok kontrolü yapar, Stock tablosuna yazar
        (bool success, string message) Add(Sale sale);
    }
}
