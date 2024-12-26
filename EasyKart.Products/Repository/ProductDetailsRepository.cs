using EasyKart.Shared.Models;
using Microsoft.Azure.Cosmos;

namespace EasyKart.Products.Repository
{
    public class ProductDetailsRepository : IProductDetailsRepository
    {
        IConfiguration _configuration;
        private readonly string _cosmosEndpoint;
        private readonly string _cosmosKey;
        private readonly string _databaseId;
        private readonly CosmosClient _cosmosClient;

        private readonly Container _container;
        private const string _containerId = "ProductDetails";
        private const string _partitionKey = "/productId";
        

        public ProductDetailsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _cosmosEndpoint = _configuration["CosmosDB:endpoint"];
            _cosmosKey = _configuration["CosmosDB:authKey"];
            _databaseId = _configuration["CosmosDB:databaseId"];

            _cosmosClient = new CosmosClient(_cosmosEndpoint, _cosmosKey);
            _container = _cosmosClient.GetContainer(_databaseId, _containerId);
        }

        public async Task<ProductDetails> GetProductDetailsAsync(string productId)
        {
            try
            {
                var query = $"SELECT * FROM c WHERE c.productId = @propertyValue";
                var queryDefinition = new QueryDefinition(query).WithParameter("@propertyValue", productId);

                var queryResultSetIterator = _container.GetItemQueryIterator<ProductDetails>(queryDefinition);

                if (queryResultSetIterator.HasMoreResults)
                {
                    var response = await queryResultSetIterator.ReadNextAsync();
                    return response.Resource.FirstOrDefault(); // Return the first matching item, or null if none exist.
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
