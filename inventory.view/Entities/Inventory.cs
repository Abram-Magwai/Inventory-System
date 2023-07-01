using Mongo.Abstracts;

namespace inventory.view.Entities
{
    public class Inventory : MongoCollection
    {
        public string SupplierId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Quantity { get; set; }
        public double Cost { get; set; }
        public string Type { get; set; } = null!;
        public DateTime ProcuredDate { get; set; }
    }
}
