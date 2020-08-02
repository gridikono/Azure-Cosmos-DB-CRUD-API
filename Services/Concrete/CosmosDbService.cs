using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;
using ToDoApp.DTO;
using ToDoApp.Models;
using ToDoApp.Services.Contract;

namespace ToDoApp.Services.Concrete
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(Item item)
        {
            await this._container.CreateItemAsync<Item>(item, new PartitionKey(item.Id));
        }

        public async Task DeleteItemAsync(Guid id)
        {
            await this._container.DeleteItemAsync<Item>(id.ToString(), new PartitionKey(id.ToString()));
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            try
            {
                ItemResponse<Item> response = await this._container.ReadItemAsync<Item>(id.ToString(), new PartitionKey(id.ToString()));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<IEnumerable<Item>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Item>(new QueryDefinition(queryString));
            List<Item> results = new List<Item>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(Guid id, ItemPutDTO item)
        {
            await this._container.UpsertItemAsync<ItemPutDTO>(item, new PartitionKey(id.ToString()));
        }
    }
}
