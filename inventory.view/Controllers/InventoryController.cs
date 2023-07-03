using inventory.view.Entities;
using inventory.view.Interfaces;
using inventory.view.Models;
using inventory.view.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace inventory.view.Controllers
{
    public class InventoryController : Controller
    {
        [BindProperty]
        public InventoryModel Inventory { get; set; } = null!;

        public bool CanReset;
        private static List<InventoryModel> DefaultInventories = new List<InventoryModel>();
        public static List<InventoryModel> Inventories { get; set; } = null!;
        public static InventorySummary Summaries { get; set; } = null!;
        public static List<string> LowStock { get; set; } = null!;
        public static List<SupplierModel> Suppliers { get; set; } = null!;

        private readonly IInventoryService _inventoryService;
        private readonly ISupplierService _supplierService;

        public InventoryController(IInventoryService inventoryService, ISupplierService supplierService)
        {
            _inventoryService = inventoryService;
            _supplierService = supplierService;
        }
        public IActionResult Index()
        {
            Summaries = _inventoryService.GetSummary().GetAwaiter().GetResult();
            LowStock = _inventoryService.GetUpdates().Result;
            Inventories = _inventoryService.GetInventories().GetAwaiter().GetResult();
            CanReset = false;
            DefaultInventories.Clear();
            CopyInventoriesLocal();

            ViewBag.Inventories = Inventories;
            ViewBag.Summaries = Summaries;
            ViewBag.LowStock = LowStock;
            ViewBag.canReset = CanReset;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            Suppliers = await _supplierService.GetSuppliers();
            ViewBag.Suppliers = Suppliers;
            return View();
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            Inventories = _inventoryService.GetInventories().GetAwaiter().GetResult();
            Suppliers = _supplierService.GetSuppliers().Result;

            ViewBag.Inventories = Inventories;
            ViewBag.Suppliers = Suppliers;

            Inventory = Inventories.AsQueryable().Where(x => x.Id == id).FirstOrDefault()!;
            if (Inventory == null) return View(viewName: "Edit");
            return View(Inventory);
        }
        public async Task<IActionResult> Add(InventoryModel inventory) {
            ModelState.Remove("Id");
            if(!ModelState.IsValid) 
                return View(viewName: "Add");
            await _inventoryService.New(inventory);

            Summaries = _inventoryService.GetSummary().GetAwaiter().GetResult();
            LowStock = _inventoryService.GetUpdates().Result;
            Inventories = _inventoryService.GetInventories().GetAwaiter().GetResult();
            CanReset = false;
            CopyInventoriesLocal();

            ViewBag.Inventories = Inventories;
            ViewBag.Summaries = Summaries;
            ViewBag.LowStock = LowStock;
            ViewBag.canReset = CanReset;

            return RedirectToAction(actionName: "Index");
        }
        [HttpPost]
        public IActionResult Update(InventoryModel inventory)
        {
            if (!ModelState.IsValid)
                return View(viewName: "Edit");

            _inventoryService.Update(inventory);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(string id)
        {
            await _inventoryService.Remove(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult SearchFilter(Search search)
        {
            if (search.Text == null || search.Text == string.Empty)
            {
                ViewBag.Inventories = Inventories;
                ViewBag.Summaries = Summaries;
                ViewBag.LowStock = LowStock;
                ViewBag.canReset = CanReset;
                return View(viewName: "Index");
            }
            CanReset = true;
            Inventories.Clear();

            switch(search.Filter) {
                case "name":
                    DefaultInventories.ForEach(inventory => {
                        if (inventory.Name.ToLower().Contains(search.Text.ToLower()))
                            Inventories.Add(inventory);
                    });
                    break;
                default:
                    DefaultInventories.ForEach(inventory => {
                        if (inventory.Type.ToLower().Contains(search.Text.ToLower()))
                            Inventories.Add(inventory);
                    });
                    break;
            }
            Summaries = _inventoryService.GetSummary().GetAwaiter().GetResult();
            LowStock = _inventoryService.GetUpdates().Result;

            ViewBag.Summaries = Summaries;
            ViewBag.LowStock = LowStock;
            ViewBag.CanReset = CanReset;
            ViewBag.Inventories = Inventories;

            return View(viewName: "Index");
        }
        public IActionResult SearchName(string name)
        {
            var names = Inventories.Where(p => p.Name.ToLower().Contains(name.ToLower())).Select(p => p.Name).ToList();
            return new JsonResult(names);
        }
        [HttpPost]
        public IActionResult ResetSearchFilter()
        {
            return RedirectToAction("Index");
        }
        private void CopyInventoriesLocal()
        {
            //searches will be made against DefaultInventories list
            Inventories.ForEach(inventory => DefaultInventories.Add(inventory));
        }
    }
}
