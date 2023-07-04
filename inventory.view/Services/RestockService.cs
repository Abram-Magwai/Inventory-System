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

        public async Task<List<RestockModel>> GetRestocks() {
            var restocks = await _restockRepository.GetAsync();
            var restockModels = new List<RestockModel>();
            restocks.ForEach(async restock => {
                string inventoryName = (await _inventoryRepository.GetAsync(restock.InventoryId))!.Name;
                restockModels.Add(new RestockModel
                {
                    Id = restock.Id,
                    Name = inventoryName,
                    Quantity = restock.Quantity
                });
            });
            return restockModels;
        }
        public async Task<RestockModel?> GetRestockByInventoryId(string id) {
            Restock restock = (await _restockRepository.GetAsync(id))!;
            if (restock == null) return null;
            string inventoryName = (await _inventoryRepository.GetAsync(restock.InventoryId))!.Name;
            return new RestockModel { Id = restock.Id, Name = inventoryName, Quantity = restock.Quantity};
        }

        public async Task<bool> Create(RestockModel restockModel)
        {
            string inventoryId = _inventoryRepository.AsQueryable().Where(inventory => inventory.Name.Equals(restockModel.Name)).FirstOrDefault()!.Id;
            Restock restock = _restockRepository.AsQueryable().Where(restock => restock.InventoryId == inventoryId).FirstOrDefault()!;
            bool restockAlreadyExists = restock == null ? false : true;
            if (inventoryId == null || restockAlreadyExists) return false;
             restock = new() { Id = string.Empty, InventoryId = inventoryId, Quantity = restockModel.Quantity};
            await _restockRepository.CreateAsync(restock);
            return true;
        }

        public async Task<bool> Update(RestockModel restockModel)
        {
            Restock restock = (await _restockRepository.GetAsync(restockModel.Id!))!;
            if(restock == null) return false;
            restock.Quantity = restockModel.Quantity;
            await _restockRepository.UpdateAsync(restock.Id, restock);
            return true;
        }

        public async Task<bool> Delete(string id)
        {
            await _restockRepository.RemoveAsync(id);
            return true;
        }
    }
}
