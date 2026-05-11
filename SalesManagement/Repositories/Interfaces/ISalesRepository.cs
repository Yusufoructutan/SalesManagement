using SalesManagement.Models;

namespace SalesManagement.Repositories.Interfaces
{

    public interface ISalesRepository
    {

        // Tüm satışları getirir
        public IEnumerable<Sale> GetAll();


        // Yeni satış ekler
        public void Add(Sale sale);
    }
}
