using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.DTO;
using ToDoApp.Models;
using ToDoApp.Services.Contract;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosDbService;
        public ToDoController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string query = "SELECT* FROM c";
            return Ok(await _cosmosDbService.GetItemsAsync(query));
        }

        [HttpGet("{id:length(24)}", Name = "GetItem")]
        public ActionResult<Item> Get(Guid id)
        {
            var item = _cosmosDbService.GetItemAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [Route("")]
        [HttpPost]
        public IActionResult CreateItem(ItemPostDTO itemDTO)
        {
            Item itemObj = new Item();
            if (ModelState.IsValid)
            {
                var Id = Guid.NewGuid().ToString();
                itemObj.Id = Id;
                itemObj.Name = itemDTO.Name;
                itemObj.Description = itemDTO.Description;
                itemObj.Completed = false;
                itemObj.CraetedBy = "N/A";
                itemObj.EditedBy = "N/A";
                itemObj.CreatedDate = DateTime.Now;
                itemObj.EditedDate = DateTime.Now;
                _cosmosDbService.AddItemAsync(itemObj);
            }

            //return CreatedAtRoute("GetItem", new { id = item.Id.ToString() }, item);
            return Ok(itemObj);
        }

        [HttpPut]
        public IActionResult Update(Guid id, ItemPutDTO itemIn)
        {
            var item = _cosmosDbService.GetItemAsync(id);

            if (item == null)
            {
                return NotFound();
            }
            itemIn.EditedBy = "N/A";
            itemIn.EditedDate = DateTime.Now;

            _cosmosDbService.UpdateItemAsync(id, itemIn);

            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            var item = _cosmosDbService.GetItemAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            _cosmosDbService.DeleteItemAsync(id);

            return NoContent();
        }
    }
}