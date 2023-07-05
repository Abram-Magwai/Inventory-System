using inventory.view.Interfaces;
using inventory.view.Models;
using jsreport.AspNetCore;
using jsreport.Types;
using Microsoft.AspNetCore.Mvc;

namespace inventory.view.Controllers
{
    public class ReportsController : Controller
    {
        public ReportModel ReportModel { get; set; } = null!;

        private readonly IInventoryService _inventoryService;
        private readonly IRestockService _restockService;
        private readonly ISupplierService _supplierService;

        public ReportsController(IInventoryService inventoryService, IRestockService restockService, ISupplierService supplierService)
        {
            _inventoryService = inventoryService;
            ReportModel = new();
            _restockService = restockService;
            _supplierService = supplierService;
        }
        [MiddlewareFilter(typeof(JsReportPipeline))]
        public async Task<IActionResult> Index()
        {
            HttpContext.JsReportFeature().Recipe(Recipe.ChromePdf);

            ReportModel.InventorySummary = await _inventoryService.GetSummary();
            ReportModel.Inventories = await _inventoryService.GetInventories();
            ReportModel.Restocks = await _restockService.GetRestocks();
            ReportModel.Suppliers = await _supplierService.GetSuppliers();

            return View(ReportModel);
        }
    }
}
