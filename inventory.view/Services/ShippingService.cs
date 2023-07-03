using inventory.view.Entities;
using inventory.view.Interfaces;
using inventory.view.Models;
using Mongo.Interfaces;

namespace inventory.view.Services
{
    public class ShippingService : IShippingService
    {
        private readonly IMongoRepository<Shipment> _shipmentsRepository;
        private readonly IMongoRepository<Inventory> _inventoriesRepository;

        public ShippingService(IMongoRepository<Shipment> shipmentsRepository, IMongoRepository<Inventory> inventoriesRepository) { 
            _shipmentsRepository = shipmentsRepository;
            _inventoriesRepository = inventoriesRepository;
        }
        public async Task<bool> Create(ShipmentModel shipmentModel)
        {
            Inventory inventory = (await _inventoriesRepository.GetAsync(shipmentModel.InventoryId))!;

            if (inventory == null) return false;
            if (inventory.Quantity == shipmentModel.Quantity)
                await _inventoriesRepository.RemoveAsync(inventory.Id);
            else
            {
                inventory.Quantity -= shipmentModel.Quantity;
                await _inventoriesRepository.UpdateAsync(inventory.Id, inventory);
            }
            return true;
        }
        public bool InventoryIsAvailable(string inventoryName, int quantity)
        {
            int available = _inventoriesRepository.AsQueryable().Where(inventory => inventory.Name == inventoryName).Sum(inventory => inventory.Quantity);
            return quantity <= available;
        }
    }
}
