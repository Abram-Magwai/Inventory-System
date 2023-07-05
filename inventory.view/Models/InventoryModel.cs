using inventory.view.Attributes;
using Mongo.Attributes;
using System.ComponentModel.DataAnnotations;

namespace inventory.view.Models
{
    //[Future]
    public class InventoryModel
    {
        public string Id { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Type { get; set; } = null!;
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Cost { get; set; }
        [Required]
        public string Supplier { get; set; } = null!;
        //[Required]
        //public string HasExpiry { get; set; } = "No";
        //public DateTime ExpiryDate { get; set; } = DateTime.Now.AddMonths(2);
        public string ProcuredDate { get; set; } = string.Empty;
    }
}
