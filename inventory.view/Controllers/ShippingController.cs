using inventory.view.Entities;
using inventory.view.Interfaces;
using inventory.view.Models;
using inventory.view.Services;
using Microsoft.AspNetCore.Mvc;

namespace inventory.view.Controllers
{
    public class ShippingController : Controller
    {
        [BindProperty]
        public ShipmentModel Shipment { get; set; } = null!;
        public List<InventoryModel> Inventories { get; set; } = null!;

        private readonly IInventoryService _inventoryService;
        private readonly IShippingService _shippingService;

        public ShippingController(IInventoryService inventoryService, IShippingService shippingService)
        {
            _inventoryService = inventoryService;
            _shippingService = shippingService;
        }
        public IActionResult Index()
        {

            Inventories = _inventoryService.GetInventories().GetAwaiter().GetResult();
            ViewBag.Inventories = Inventories;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create(string id)
        {
            InventoryModel inventory = (await _inventoryService.GetInventoryById(id))!;
            if (inventory == null) return View();
            Shipment = new() { InventoryId = inventory.Id, InventoryName = inventory.Name, Quantity = inventory.Quantity };
            return View(Shipment);
        }
        public async Task<IActionResult> Create(ShipmentModel shipment)
        {
            if (!ModelState.IsValid) {
                Inventories = await _inventoryService.GetInventories();
                ViewBag.Inventories = Inventories;
                return View(viewName: "Create");
            };
            await _shippingService.Create(shipment);
            return RedirectToAction(actionName: "Index", controllerName: "Shipping");
        }
    }
}
