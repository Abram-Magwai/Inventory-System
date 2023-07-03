using inventory.view.Entities;
using inventory.view.Interfaces;
using inventory.view.Models;
using Mongo.Interfaces;

namespace inventory.view.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IMongoRepository<Supplier> _suppliersRepository;
        public SupplierService(IMongoRepository<Supplier> suppliersRepository)
        {
            _suppliersRepository = suppliersRepository;
        }
        public async Task<SupplierModel?> GetSupplier(string id)
        {
            Supplier? supplier = await _suppliersRepository.GetAsync(id);
            if (supplier == null) return null;
            SupplierModel supplierModel = new () { Id = supplier.Id, Name = supplier.Name, Email = supplier.Email};
            return supplierModel;
        }

        public async Task<List<SupplierModel>> GetSuppliers()
        {
            List<Supplier> supplier = await _suppliersRepository.GetAsync();
            List<SupplierModel> supplierModels = new();
            supplier.ForEach(sup => supplierModels.Add(new SupplierModel {Id = sup.Id, Name = sup.Name, Email = sup.Email}));
            return supplierModels;
        }
        public async Task<bool> Create(SupplierModel  supplierModel)
        {
            var supplier = SupplierModelToSupplier(supplierModel);
            await _suppliersRepository.CreateAsync(supplier);
            return true;
        }
        public async Task<bool> Update(SupplierModel supplierModel)
        {
            Supplier supplier = SupplierModelToSupplier(supplierModel);
            await _suppliersRepository.UpdateAsync(supplierModel.Id!, supplier);
            return true;
        }
        public async Task<bool> Delete(string id)
        {
            Supplier? supplier = await _suppliersRepository.GetAsync(id);
            if (supplier == null) return false;
            await _suppliersRepository.RemoveAsync(id);
            return true;
        }
        private Supplier SupplierModelToSupplier(SupplierModel supplierModel)
        {
            Supplier supplier = new() { Id = supplierModel.Id ?? "", Name = supplierModel.Name, Email = supplierModel.Email };
            return supplier;
        }
    }
}
