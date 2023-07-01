using Microsoft.Extensions.Options;
using Mongo.Abstracts;
using Mongo.Attributes;
using Mongo.Interfaces;
using Mongo.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace inventory.infrastructure.Repositories
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : MongoCollection
    {
        private readonly IMongoCollection<TDocument> _collection;
        public MongoRepository(IOptions<DbSettings> dbSettings)
        {
            var client = new MongoClient(dbSettings.Value.ConnectionString);
            var database = client.GetDatabase(dbSettings.Value.DatabaseName);
            _collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }
        string GetCollectionName(Type type)
        {
            return ((CollectionNameAttribute)type.GetCustomAttributes(typeof(CollectionNameAttribute), true)[0]).Name;
        }
        public async Task<List<TDocument>> GetAsync() =>
        await _collection.Find(_ => true).ToListAsync();

        public async Task<TDocument?> GetAsync(string id) =>
            await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(TDocument model) =>
            await _collection.InsertOneAsync(model);

        public async Task UpdateAsync(string id, TDocument updatedModel) =>
            await _collection.ReplaceOneAsync(x => x.Id == id, updatedModel);

        public async Task RemoveAsync(string id) =>
            await _collection.DeleteOneAsync(x => x.Id == id);
        public TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).FirstOrDefault();
        }
        public IQueryable<TDocument> AsQueryable()
        {
            return _collection.AsQueryable();
        }
    }
}
