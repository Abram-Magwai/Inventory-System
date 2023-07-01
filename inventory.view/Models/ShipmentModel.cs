using System.ComponentModel.DataAnnotations;

namespace inventory.view.Models
{
    public class ShipmentModel
    {
        [Display(Name = "Name")]
        public string InventoryName { get; set; } = null!;
        public string Customer { get; set; } = null!;
        public int Quantity { get; set; }
        public DateTime ShippingDate { get; set; }
    }
}
