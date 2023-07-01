using inventory.view.Entities;
using inventory.view.Interfaces;
using inventory.view.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.ObjectModelRemoting;

namespace inventory.view.Controllers
{
    public class SettingsController : Controller
    {
        public List<SupplierModel> Suppliers { get; set; } = null!;
        public List<RestockModel> Restocks { get; set; } = null!;

        private readonly ISupplierService _supplierService;
        private readonly IRestockService _restockService;

        public SettingsController(ISupplierService supplierService, IRestockService restockService)
        {
            _supplierService = supplierService;
            _restockService = restockService;
        }
        public async Task<IActionResult> Index()
        {
            Suppliers = await _supplierService.GetSuppliers();
            Restocks = await _restockService.GetRestocks();

            ViewBag.Suppliers = Suppliers;
            ViewBag.Restocks = Restocks;

            return View();
        }
    }
}
