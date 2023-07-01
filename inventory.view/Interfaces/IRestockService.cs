using inventory.view.Models;

namespace inventory.view.Interfaces
{
    public interface IRestockService
    {
        public Task<List<RestockModel>> GetRestocks();
        public Task<RestockModel> GetRestockByInventoryId(string id);
        public Task<bool> Create(RestockModel restockModel);
        public Task<bool> Update(RestockModel restockModel);
        public Task<bool> Delete(string id);
    }
}
