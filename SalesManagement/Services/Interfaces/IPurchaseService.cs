using SalesManagement.Models;
using SalesManagement.ViewModels;

namespace SalesManagement.Services.Interfaces
{

    public interface IPurchaseService
    {

        // Tüm satın alımları ViewModel olarak döner
        public IEnumerable<PurchaseViewModel> GetAll();

  
        // Satın alım ekler ve Stock tablosuna yazar
        public void Add(Purchase purchase);
    }
}
