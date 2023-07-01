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
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            Inventories = await _inventoryService.GetInventories();
            ViewBag.Inventories = Inventories;
            return View();
        }
        public IActionResult Create(ShipmentModel shipment)
        {
            if (!ModelState.IsValid) return View(viewName: "Create");
            _shippingService.Create(shipment);
            return RedirectToAction(actionName: "Index", controllerName: "Inventory");
        }
    }
}
