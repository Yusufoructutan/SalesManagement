using SalesManagement.Models;

namespace SalesManagement.Repositories.Interfaces
{
 
    public interface IPurchaseRepository
    {
        // Tüm satın alımları getirir
        public IEnumerable<Purchase> GetAll();

   
        // Yeni satın alım ekler
        public void Add(Purchase purchase);
    }
}
