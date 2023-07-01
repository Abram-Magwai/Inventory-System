using Mongo.Abstracts;
using Mongo.Attributes;

namespace inventory.view.Entities
{
    [CollectionName("Suppliers")]
    public class Supplier : MongoCollection
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
