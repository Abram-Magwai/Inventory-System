using inventory.view.Models;

namespace inventory.view.Interfaces
{
    public interface IShippingService
    {
        public Task<bool> Create(ShipmentModel shipment);
        bool InventoryIsAvailable(string inventoryName, int quantity);
    }
}
