using Mongo.Abstracts;
using Mongo.Attributes;

namespace inventory.view.Entities
{
    [CollectionName("Expiries")]
    public class Expiration : MongoCollection
    {
        public string InventoryId { get; set; } = null!;
        public DateTime Date { get;set; }
    }
}
