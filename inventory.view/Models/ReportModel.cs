namespace inventory.view.Models
{
    public class ReportModel
    {
        public InventorySummary InventorySummary { get; set; } = null!;
        public List<InventoryModel> Inventories { get; set; } = null!;
        public List<RestockModel> Restocks { get; set; } = null!;
        public List<SupplierModel> Suppliers { get; set; } = null!;
    }
}
