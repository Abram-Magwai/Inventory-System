using System.ComponentModel.DataAnnotations;

namespace inventory.view.Models
{
    public class SupplierModel
    {
        public string? Id { get;set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
    }
}
