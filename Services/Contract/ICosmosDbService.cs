using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.DTO;
using ToDoApp.Models;

namespace ToDoApp.Services.Contract
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<Item>> GetItemsAsync(string query);
        Task<Item> GetItemAsync(Guid id);
        Task AddItemAsync(Item item);
        Task UpdateItemAsync(Guid id, ItemPutDTO item);
        Task DeleteItemAsync(Guid id);
    }
}
