using inventory.view.Entities;
using inventory.view.Interfaces;
using inventory.view.Models;

namespace inventory.view.Services
{
    public class FakeSupplierService : ISupplierService
    {
        public Task<bool> Create(SupplierModel supplier)
        {
            return Task.Run(() => true);
        }

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<SupplierModel?> GetSupplier(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<SupplierModel>> GetSuppliers()
        {
            var suppliers = new List<SupplierModel> { 
                new SupplierModel
                {
                    Name = "KFC",
                    Email = "kfc@kfc.com"
                },
                new SupplierModel
                {
                    Name = "Mac D",
                    Email = "d@macd.com"
                }
            };
            return Task.Run(() => suppliers);
        }

        public Task<bool> Update(SupplierModel supplier)
        {
            throw new NotImplementedException();
        }
    }
}