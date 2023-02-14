using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.DTO;
using Play.Catalog.Service.Entities;
using Play.Catalog.Service.ItemRepositories;

namespace Play.Catalog.Service.Controllers
{

    [ApiController]
    [Route("Items")]
    public class ItemController : ControllerBase
    {
        private readonly ItemRepository itemRepository = new();                                               
      
        [HttpGet]
        public async Task < IEnumerable<ItemDto>> GetAsync()
        {
            var items = (await itemRepository.GetAllAsync())
                .Select(item => item.AsDto());
            return items;
        }
        // Get /items/{id}
        [HttpGet("{id}")]
        public async Task < ActionResult< ItemDto>> GetByIdAsync(Guid id)
        {
            var item = await itemRepository.GetAsyn (id);
            if(item == null)
            {
                return NotFound();
            }
            return item.AsDto();
        }
        [HttpPost]
        public async Task< ActionResult<ItemDto>>PostAsync(CreateItemDto createItemDto)
        {
            var item = new Item
            {
                Name = createItemDto.Name,
                Description = createItemDto.Description,
                Price = createItemDto.Price,
                CreatedDate= DateTimeOffset.UtcNow,

            };
            await itemRepository.CreateAsync (item);


            return CreatedAtAction (nameof(GetByIdAsync), new {id = item.Id},item);
        }
        //Put /item/{id}
        [HttpPut("{id}")]
        public async Task < ActionResult<ItemDto>> PutAsync  (UpdateItemDto updateItemDto, Guid id )
        {
           var existingItem = await itemRepository.GetAsyn (id);
            if(existingItem == null)
            {
                return NotFound ();

            }
            existingItem.Name = updateItemDto.Name;
            existingItem.Description = updateItemDto.Description;
            existingItem.Price = updateItemDto.Price;
            existingItem.CreatedDate = DateTimeOffset.UtcNow;
            await itemRepository.UpdateAsync (existingItem);
            return NoContent();
         
            
        }
        // delete /items/{id}
        [HttpDelete ("{id}")]
        public async Task <IActionResult> DeleteAsync(Guid id)
        {
            var item= await itemRepository.GetAsyn(id);
            if (item == null)
            {
                return NotFound();

            }
            await itemRepository.RemoveAsync (item.Id);

            return NoContent();
        }
    }
}
