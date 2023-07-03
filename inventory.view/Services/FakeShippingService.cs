using inventory.view.Interfaces;
using inventory.view.Models;

namespace inventory.view.Services
{
    public class FakeShippingService : IShippingService
    {
        public Task<bool> Create(ShipmentModel shipment)
        {
            return Task.Run(() => true);
        }

        public bool InventoryIsAvailable(string inventoryName, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
