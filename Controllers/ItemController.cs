using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.DTO;

namespace Play.Catalog.Service.Controllers
{

    [ApiController]
    [Route("Items")]
    public class ItemController : ControllerBase
    {
        private static readonly List<ItemDto> items = new List<ItemDto>()
        {
            new ItemDto (Guid.NewGuid() ,"Potion", "Restore a small amoun of HP",5,DateTimeOffset.UtcNow),
            new ItemDto (Guid.NewGuid() ,"Antidote", "Cures poison",7,DateTimeOffset.UtcNow),
            new ItemDto (Guid.NewGuid() ,"Bronze sword", "Deals with small amount of damage",20,DateTimeOffset.UtcNow)
        };
        [HttpGet]
        public IEnumerable<ItemDto> Get()
        {
            return items;
        }
        // Get /items/{id}
        [HttpGet("{id}")]
        public ItemDto GetById(Guid id)
        {
            var item = items.Where (item => item.Id == id).SingleOrDefault ();
            return item;
        }
        [HttpPost]
        public ActionResult<ItemDto>Post(CreateItemDto createItemDto)
        {
            var itemDto = new ItemDto (Guid.NewGuid(),createItemDto.Name, createItemDto.Description, createItemDto.Price, DateTimeOffset.UtcNow);
            items.Add (itemDto);
            return CreatedAtAction (nameof(GetById), new {id = itemDto.Id},items);
        }
        //Put /item/{id}
        [HttpPut("{id}")]
        public ActionResult<ItemDto> Put  (UpdateItemDto updateItemDto, Guid id )
        {
         var existingitem = items.Where (item => item.Id == id).SingleOrDefault ();
            var updateditem = existingitem with
            {
                Name = updateItemDto.Name,
                Description = updateItemDto.Description,
                Price = updateItemDto.Price,
            };
            var index = items.FindIndex (existingitem => existingitem.Id == id);
            items[index ] =  updateditem;
            return NoContent();
         
            
        }
        // delete /items/{id}
        [HttpDelete ("{id}")]
        public IActionResult Delete(Guid id)
        {
          
            var index = items.FindIndex(item => item.Id == id);

            items.RemoveAt(index);
            return NoContent();
        }
    }
}
