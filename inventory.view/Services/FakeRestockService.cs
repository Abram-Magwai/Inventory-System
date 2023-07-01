using inventory.view.Interfaces;
using inventory.view.Models;

namespace inventory.view.Services
{
    public class FakeRestockService : IRestockService
    {
        public async Task<List<RestockModel>> GetRestocks() { 
            return new List<RestockModel>() { 
                new RestockModel { Name = "Washing Machine", Quantity = 200},
                new RestockModel { Name = "Screws", Quantity = 4000}
            }; 
        }
        public async Task<RestockModel> GetRestockByInventoryId(string id) { return new RestockModel(); }

        public Task<bool> Create(RestockModel restockModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(RestockModel restockModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
