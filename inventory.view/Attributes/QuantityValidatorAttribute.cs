using inventory.view.Controllers;
using inventory.view.Entities;
using System.ComponentModel.DataAnnotations;

namespace inventory.view.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class QuantityValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not Shipment) return new ValidationResult("Object not of type shipment");
            if (((Shipment)value).Quantity > 50) return new ValidationResult("Not enough stock", new List<string>() { "Quantity" });
            return ValidationResult.Success!;
        }
    }
}
