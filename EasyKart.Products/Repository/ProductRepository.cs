using EasyKart.Shared.Models;
using Microsoft.Azure.Cosmos;

namespace EasyKart.Products.Repository
{
    public class ProductRepository : IProductRepository
    {
        IConfiguration _configuration;
        private readonly string _cosmosEndpoint;
        private readonly string _cosmosKey;
        private readonly string _databaseId;
        private readonly string _containerId;
        private readonly string _partitionKey;

        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;


        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _cosmosEndpoint = _configuration["CosmosDB:endpoint"];
            _cosmosKey = _configuration["CosmosDB:authKey"];
            _databaseId = _configuration["CosmosDB:databaseId"];
            _containerId = _configuration["CosmosDB:containerId"];
            _partitionKey = _configuration["CosmosDB:partitionKey"];

            _cosmosClient = new CosmosClient(_cosmosEndpoint, _cosmosKey);
            _container = _cosmosClient.GetContainer(_databaseId, _containerId);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            List<Product> products = new List<Product>();

            try
            {
                QueryDefinition query = new QueryDefinition("SELECT * FROM c");
                FeedIterator<Product> queryResultSetIterator = _container.GetItemQueryIterator<Product>(query);
               

                while (queryResultSetIterator.HasMoreResults)
                {
                    FeedResponse<Product> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    products.AddRange(currentResultSet);
                }
            }
            catch (Exception ex)
            {

                throw;
            }           

            return products;
        }

        public async Task<Product> GetProductAsync(string productId, string categoryId)
        {
            try
            {
                ItemResponse<Product> response = await _container.ReadItemAsync<Product>(productId, new PartitionKey(categoryId));
                return response.Resource;
            }
            catch (CosmosException ex) 
            {
                return null;
            }
        }

        // get products by product ids
        public async Task<List<Product>> GetProductsByIdsAsync(List<Guid> productIds)
        {
            List<Product> products = new List<Product>();
            try
            {
                List<Product> allprod = await GetProductsAsync();
                products = allprod.Where(p => productIds.Contains(p.Id)).ToList();
            }
            catch (CosmosException ex)
            {
                return null;
            }
            return products;
        }

    }
}
