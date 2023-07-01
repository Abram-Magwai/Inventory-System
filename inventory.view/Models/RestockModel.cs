using System.ComponentModel.DataAnnotations;

namespace inventory.view.Models
{
    public class RestockModel
    {
        public string? Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public int Quantity { get; set; }
    }
}
