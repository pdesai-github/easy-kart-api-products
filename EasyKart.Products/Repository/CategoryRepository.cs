using EasyKart.Shared.Models;
using Microsoft.Azure.Cosmos;

namespace EasyKart.Products.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        IConfiguration _configuration;
        private readonly string _cosmosEndpoint;
        private readonly string _cosmosKey;
        private readonly string _databaseId;
        private readonly CosmosClient _cosmosClient;

        private readonly Container _container;
        private const string _containerId = "Categories";

        public CategoryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _cosmosEndpoint = _configuration["CosmosDB:endpoint"];
            _cosmosKey = _configuration["CosmosDB:authKey"];
            _databaseId = _configuration["CosmosDB:databaseId"];

            _cosmosClient = new CosmosClient(_cosmosEndpoint, _cosmosKey);
            _container = _cosmosClient.GetContainer(_databaseId, _containerId);
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            try
            {
                var query = $"SELECT * FROM c";
                var queryDefinition = new QueryDefinition(query);
                var queryResultSetIterator = _container.GetItemQueryIterator<Category>(queryDefinition);
                if (queryResultSetIterator.HasMoreResults)
                {
                    var response = await queryResultSetIterator.ReadNextAsync();
                    return response.Resource.ToList<Category>(); // Return the first matching item, or null if none exist.
                }
                return null;
            }
            catch (CosmosException ex)
            {
                return null;
            }
        }
    }
}
