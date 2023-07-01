using inventory.view.Entities;
using inventory.view.Models;
using System.ComponentModel.DataAnnotations;

namespace inventory.view.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class FutureAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return false;
            InventoryModel inventory;
            try
            {
                inventory = (InventoryModel)value;
            }
            catch (Exception e) { Console.WriteLine(e); return false; }

            if (inventory.HasExpiry == "Yes")
            {
                if (inventory.ExpiryDate > DateTime.Now)
                    return true;
                return false;
            }
            return true;
        }
    }
}
