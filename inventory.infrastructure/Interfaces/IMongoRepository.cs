using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mongo.Interfaces
{
    public interface IMongoRepository <TDocument>
    {
        public Task<List<TDocument>> GetAsync();
        public Task<TDocument?> GetAsync(string id);
        Task CreateAsync(TDocument obj);
        Task UpdateAsync(string id, TDocument obj);
        Task RemoveAsync(string id);
        IQueryable<TDocument> AsQueryable();
    }
}
