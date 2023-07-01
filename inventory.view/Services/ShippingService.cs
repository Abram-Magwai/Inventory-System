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
            string inventoryId = _inventoriesRepository.AsQueryable().Where(inventory => inventory.Name.Equals(shipmentModel.InventoryName)).FirstOrDefault()!.Id;

            if (inventoryId == null) return false;

            var shipment = new Shipment {
                InventoryId = inventoryId,
                Customer = shipmentModel.Customer,
                Quantity = shipmentModel.Quantity,
                ShippingDate = DateTime.Now
            };
            await _shipmentsRepository.CreateAsync(shipment);
            return true;
        }
    }
}
