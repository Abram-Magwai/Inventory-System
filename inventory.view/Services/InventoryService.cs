﻿using inventory.view.Entities;
using inventory.view.Interfaces;
using inventory.view.Models;
using Mongo.Interfaces;

namespace inventory.view.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IMongoRepository<Inventory> _inventoriesRepository;
        private readonly IMongoRepository<Supplier> _suppliersRepository;

        public InventoryService(IMongoRepository<Inventory> inventoriesRepository, IMongoRepository<Supplier> supplierRepository)
        {
            _inventoriesRepository = inventoriesRepository;
            _suppliersRepository = supplierRepository;
        }

        public async Task<InventoryModel?> GetInventoryById(string id)
        {
            Inventory inventory = (await _inventoriesRepository.GetAsync(id))!;
            if (inventory == null) return null;

            string supplierName = _suppliersRepository.AsQueryable().Where(supplier => supplier.Id == id).FirstOrDefault()!.Name;
            InventoryModel model = new InventoryModel
            {
                Name = inventory!.Name,
                Type = inventory.Type,
                Quantity = inventory.Quantity,
                Cost = inventory.Cost,
                Supplier = supplierName,
            };
            return model;
        }

        public async Task<InventorySummary> GetSummary()
        {
            List<Inventory> inventories = await _inventoriesRepository.GetAsync();
            int totalQuantity = 0;
            double totalCost = 0.00;

            inventories.ForEach(inv => { totalQuantity += inv.Quantity; totalCost += inv.Cost; });
            return new InventorySummary { TotalCost = totalCost, TotalInventory = totalQuantity };
        }
        public async Task<List<InventoryModel>> GetInventories()
        {
            var inventories = await _inventoriesRepository.GetAsync();
            List<InventoryModel> inventoryModels = new List<InventoryModel>();
            inventories.ForEach(inventory => inventoryModels.Add(
                new InventoryModel {
                    Name = inventory.Name,
                    Type = inventory.Type,
                    Quantity = inventory.Quantity,
                    Cost = inventory.Cost
                }    
            ));
            return inventoryModels;
        }

        public Task<List<String>> GetUpdates()
        {
            //check restock rules agains inventoryQuantity, check for everyshipment
            return Task.Run(() => new List<string>());
        }

        public async Task<bool> New(InventoryModel inventoryModel)
        {
            Inventory inventory = InventoryModelToInventory(inventoryModel)!;
            if (inventory == null) return false;
            try
            {
                await _inventoriesRepository.CreateAsync(inventory);
            }catch (Exception ex) { 
                Console.WriteLine(ex.ToString());
                return false; 
            }
            return true;
        }

        public async Task<bool> Remove(string inventoryId)
        {
            await _inventoriesRepository.RemoveAsync(inventoryId);
            return true;
        }

        public async Task<bool> Ship(ShipmentModel shipment)
        {
           string inventoryId = _inventoriesRepository.AsQueryable().Where(inventory => inventory.Name.Equals(shipment.InventoryName)).FirstOrDefault()!.Id;

            if (inventoryId == null) return false;

            //update inventory details

            //go through list of inventories then if inventory quantity is <= shipment quantity then
            List<Inventory> inventories = await _inventoriesRepository.GetAsync();
            inventories.ForEach(async inventory => { 
                int inventoryQuantity = inventory.Quantity;
                int shipmentQuantity = shipment.Quantity;

                if (inventoryQuantity <= shipmentQuantity)
                {
                    shipment.Quantity -= inventory.Quantity;
                    await _inventoriesRepository.RemoveAsync(inventory.Id);
                    if (shipment.Quantity == 0) return;
                }
                else
                {
                    inventory.Quantity -= shipment.Quantity;
                    await _inventoriesRepository.UpdateAsync(inventoryId, inventory);
                }
            });
            return true;
        }
        private async void RemoveInventory(Shipment shipment)
        {
            //TODO: optimize to only go through limited inventories
            List<Inventory> inventories = await _inventoriesRepository.GetAsync();
            foreach (Inventory inventory in inventories) { 
                if(shipment.Quantity >= inventory.Quantity)
                {
                    shipment.Quantity -= inventory.Quantity;
                    await _inventoriesRepository.RemoveAsync(inventory.Id!);
                }else
                {
                    inventory.Quantity -= shipment.Quantity;
                    await _inventoriesRepository.UpdateAsync(inventory.Id!, inventory);
                }
            }
        }
        private int GetTotalInventory(string name)
        {
            List<Inventory> inventories = _inventoriesRepository.AsQueryable().Where(inv => inv.Name.Equals(name)).ToList();
            int totalInventory = 0;
            inventories.ForEach(inv => totalInventory += inv.Quantity);
            return totalInventory;
        }

        public async Task<bool> Update(InventoryModel inventoryModel)
        {
            Inventory inventory = InventoryModelToInventory(inventoryModel)!;
            if (inventory == null) return false;

            await _inventoriesRepository.UpdateAsync(inventory.Id!, inventory);
            return true;
        }
        private Inventory? InventoryModelToInventory(InventoryModel inventoryModel)
        {
            string supplierId = _suppliersRepository.AsQueryable().Where(supplier => supplier.Name == inventoryModel.Name).FirstOrDefault()!.Id;
            if (supplierId == null) return null;
            Inventory inventory = new Inventory
            {
                SupplierId = supplierId,
                Name = inventoryModel.Name,
                Quantity = inventoryModel.Quantity,
                Cost = inventoryModel.Cost,
                Type = inventoryModel.Type,
                ProcuredDate = DateTime.Now
            };
            return inventory;
        }
    }
}
