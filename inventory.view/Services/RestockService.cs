using inventory.view.Entities;
using inventory.view.Interfaces;
using inventory.view.Models;
using Mongo.Interfaces;

namespace inventory.view.Services
{
    public class RestockService : IRestockService
    {

        private readonly IMongoRepository<Restock> _restockRepository;
        private readonly IMongoRepository<Inventory> _inventoryRepository;

        public RestockService(IMongoRepository<Restock> restockRepository, IMongoRepository<Inventory> inventoryRepository)
        {
            _restockRepository = restockRepository;
            _inventoryRepository = inventoryRepository;
        }

        public async Task<List<RestockModel>> GetRestocks() { throw new NotImplementedException(); }
        public async Task<RestockModel> GetRestockByInventoryId(string id) { throw new NotImplementedException(); }

        public async Task<bool> Create(RestockModel restockModel)
        {
            string inventoryId = _inventoryRepository.AsQueryable().Where(inventory => inventory.Name.Equals(restockModel.Name)).FirstOrDefault()!.Id;
            if (inventoryId == null) return false;
            Restock restock = new() { Id = string.Empty, InventoryId = inventoryId, Quantity = restockModel.Quantity};
            await _restockRepository.CreateAsync(restock);
            return true;
        }

        public Task<bool> Update(RestockModel restockModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
