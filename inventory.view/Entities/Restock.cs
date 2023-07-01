using Mongo.Abstracts;
using Mongo.Attributes;

namespace inventory.view.Entities
{
    [CollectionName("Restock")]
    public class Restock : MongoCollection
    {
        public string InventoryId { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
