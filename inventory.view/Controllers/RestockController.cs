using inventory.view.Interfaces;
using inventory.view.Models;
using Microsoft.AspNetCore.Mvc;

namespace inventory.view.Controllers
{
    public class RestockController : Controller
    {
        [BindProperty]
        public RestockModel RestockModel { get; set; } = null!;
        public List<InventoryModel> Inventories { get; set; } = null!;

        private readonly IInventoryService _inventoryService;
        private readonly IRestockService _restockService;

        public RestockController(IInventoryService inventoryService, IRestockService restockService)
        {
            _inventoryService = inventoryService;
            _restockService = restockService;

        }
        public IActionResult Index() { 
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            Inventories = await _inventoryService.GetInventories();
            ViewBag.Inventories = Inventories;
            return View();
        }
        public async Task<IActionResult> Create(RestockModel restockModel)
        {
            if (!ModelState.IsValid) return View();
            await _restockService.Create(restockModel);
            return RedirectToAction(actionName: "Index", controllerName: "Settings");
        }
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
        public async Task<IActionResult> Edit(RestockModel restockModel)
        {
            if (!ModelState.IsValid) return View();
            await _restockService.Update(restockModel);
            return View(viewName: "Index");
        }
        public async Task<IActionResult> Delete(string id)
        {
            await _restockService.Delete(id);
            return View();
        }
    }
}
