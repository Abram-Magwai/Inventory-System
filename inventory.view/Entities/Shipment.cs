using Mongo.Abstracts;
using Mongo.Attributes;
using System.ComponentModel.DataAnnotations;

namespace inventory.view.Entities
{
    [CollectionName("Shipments")]
    public class Shipment : MongoCollection
    {
        public string InventoryId { get; set; } = null!;
        public string Customer { get; set; } = null!;
        public int Quantity { get; set; }
        public DateTime ShippingDate { get; set; }
    }
}
