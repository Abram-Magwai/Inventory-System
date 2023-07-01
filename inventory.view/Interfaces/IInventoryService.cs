using inventory.view.Entities;
using inventory.view.Models;

namespace inventory.view.Interfaces
{
    public interface IInventoryService
    {
        public Task<InventoryModel?> GetInventoryById(String id);
        public Task<InventorySummary> GetSummary();
        public Task<List<string>> GetUpdates();
        public Task<List<InventoryModel>> GetInventories();
        public Task<bool> New(InventoryModel inventory);
        public Task<bool> Ship(ShipmentModel shipment);
        public Task<bool> Remove(string inventoryId);
        public Task<bool> Update(InventoryModel inventory);
    }
}