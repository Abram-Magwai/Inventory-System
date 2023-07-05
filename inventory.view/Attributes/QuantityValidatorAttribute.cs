using inventory.view.Controllers;
using inventory.view.Entities;
using inventory.view.Interfaces;
using inventory.view.Models;
using System.ComponentModel.DataAnnotations;

namespace inventory.view.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class QuantityValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            IShippingService _shippingService = (IShippingService)validationContext.GetService(typeof(IShippingService))!;

            if (value is not ShipmentModel) return new ValidationResult("Object not of type shipment");
            ShipmentModel shipment = (ShipmentModel)value;
            bool inventoryIsAvailable = _shippingService.InventoryIsAvailable(shipment.InventoryName, shipment.Quantity);
            if (!inventoryIsAvailable) return new ValidationResult("Not enough stock", new List<string>() { "Quantity" });
            return ValidationResult.Success!;
        }
    }
}