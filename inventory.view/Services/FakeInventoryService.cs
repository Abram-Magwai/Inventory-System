using inventory.view.constants;
using inventory.view.Entities;
using inventory.view.Interfaces;
using inventory.view.Models;
using System.Runtime.CompilerServices;

namespace inventory.view.Services
{
    public class FakeInventoryService : IInventoryService
    {
        public Task<InventoryModel?> GetInventoryById(string id)
        {
            throw new NotImplementedException();
        }
         public Task<InventorySummary> GetSummary()
        {
            var summary = new InventorySummary { TotalCost = 71000, TotalInventory = 1600 };
            return Task.Run(() => summary);
        }
        public Task<List<string>> GetUpdates() {
            var updates = new List<string>() { 
                "Washing Machines running low",  
                "Bicycles running low"
            };
            return Task.Run(() => updates);
        }
        public Task<List<InventoryModel>> GetInventories()
        {
            var inventories = new List<InventoryModel>()
            {
                new InventoryModel()
                {
                    Id= "1",
                    Name = "Washing Machines",
                    Type = InventoryType.FinishedGoods,
                    Quantity = 65,
                    Cost = 27500
                },
                new InventoryModel()
                {
                    Id= "2",
                    Name = "Screws",
                    Type = InventoryType.RawMaterial,
                    Quantity = 8000,
                    Cost = 6500
                },
                new InventoryModel()
                {
                    Id= "3",
                    Name = "Machine",
                    Type = InventoryType.FinishedGoods,
                    Quantity = 65,
                    Cost = 27500
                },
                new InventoryModel()
                {
                    Id= "4",
                    Name = "Bicycles",
                    Type = InventoryType.WorkInProgress,
                    Quantity = 500,
                    Cost = 11300
                },
            };
            return Task.Run(() => inventories);
        }
        public Task<bool> New(InventoryModel inventory)
        {
            return Task.Run(() => true);
        }
        public Task<bool> Ship(ShipmentModel shipment)
        {
            return Task.Run(() => true);
        }
        public Task<bool> Remove(string inventoryId)
        {
            return Task.Run(() => true);
        }
        public Task<bool> Update(InventoryModel inventory)
        {
            return Task.Run(() => true);
        }

    }
}
