using ForumApp.API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ForumApp.API.Services
{
    public class ArticleService
    {
        private readonly IMongoCollection<Post> _articleCollection;

        public ArticleService(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _articleCollection = database.GetCollection<Post>("Posts");
        }

        public async Task<List<Post>> RetrieveAllAsync() =>
            await _articleCollection.Find(_ => true).ToListAsync();

        public async Task<Post?> RetrieveByIdAsync(string id) =>
            await _articleCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Post newArticle) =>
            await _articleCollection.InsertOneAsync(newArticle);

        public async Task UpdateAsync(string id, Post modifiedArticle) =>
            await _articleCollection.ReplaceOneAsync(x => x.Id == id, modifiedArticle);

        public async Task RemoveAsync(string id) =>
            await _articleCollection.DeleteOneAsync(x => x.Id == id);
    }
}
