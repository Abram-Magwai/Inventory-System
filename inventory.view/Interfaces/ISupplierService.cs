using inventory.view.Models;

namespace inventory.view.Interfaces
{
    public interface ISupplierService
    {
        public Task<SupplierModel?> GetSupplier(string id);
        public Task<List<SupplierModel>> GetSuppliers();
        public Task<bool> Create(SupplierModel supplier);
        public Task<bool> Update(SupplierModel supplier);
        public Task<bool> Delete(string id);
    }
}
