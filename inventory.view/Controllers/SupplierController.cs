using inventory.view.Interfaces;
using inventory.view.Models;
using Microsoft.AspNetCore.Mvc;

namespace inventory.view.Controllers
{
    public class SupplierController : Controller
    {
        [BindProperty]
        public SupplierModel SupplierModel { get; set; } = null!; 

        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> Create(SupplierModel supplierModel)
        {
            if (!ModelState.IsValid) return View();
            await _supplierService.Create(supplierModel);
            return RedirectToAction(actionName: "Index", controllerName: "Settings");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            SupplierModel = (await _supplierService.GetSupplier(id))!;
            return View(SupplierModel);
        }
        public async Task<IActionResult> Edit(SupplierModel supplierModel)
        {
            if (!ModelState.IsValid) return View();
            await _supplierService.Update(supplierModel);
            return RedirectToAction(actionName: "Index", controllerName: "Settings");
        }
        public async Task<IActionResult> Delete(string id)
        {
            await _supplierService.Delete(id);
            return RedirectToAction(actionName: "Index", controllerName: "Settings");
        }
    }
}
