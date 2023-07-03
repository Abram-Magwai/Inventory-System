using inventory.view.Attributes;
using System.ComponentModel.DataAnnotations;

namespace inventory.view.Models
{
    [QuantityValidator]
    public class ShipmentModel
    {
        [Required]
        public string InventoryId { get; set; } = null!;
        [Display(Name = "Name")]
        public string InventoryName { get; set; } = null!;
        public string Customer { get; set; } = null!;
        public int Quantity { get; set; }
        public DateTime ShippingDate { get; set; }
    }
}
