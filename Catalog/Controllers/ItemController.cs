using Catalog.Dto;
using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers {
    [ApiController] // Adds some automatic functionality for us like errors and routing, it's called an attribute
    [Route("items")] // Automatically is: /items
    public class ItemsController : ControllerBase { // ControllerBase converts this into a controller class, ez
        private readonly IItemsRepository repository; 

        public ItemsController(IItemsRepository repository) {
            this.repository = repository;
        }

        [HttpGet] // GET /items
        public async Task<IEnumerable<ItemDto>> GetItemsAsync() {
            // Note the () around the await, this is because .Select can't run immediately on a promise
            var items = (await repository.GetItemsAsync()).Select(item => item.AsDto());
            return items;
        }

        [HttpGet("{id}")] // GET /items/3fa85f64-5717-4562-b3fc-2c963f66afa6
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id) { // Add ActionResult to allows us to return various status codes depending on errors
            var item = await repository.GetItemAsync(id);

            if (item is null) {
                return NotFound(); // Returns "404 not found" for us
            }

            return item.AsDto();
        }

        [HttpPost] // POST
        public async Task<ActionResult<ItemDto>> Async(CreateItemDto itemDto) {
            Item item = new(){ 
                Id = Guid.NewGuid(), // generate ID
                Name = itemDto.Name, 
                Price = itemDto.Price, 
                CreatedDate = DateTimeOffset.UtcNow // generate Date
            };

            // Save the new resource
            await repository.CreateItemAsync(item);

            // It's convention to return the created resource
            // CreatedAtAction adds "201 Created"
            // nameof is just a title, we could add anything
            // A new id is simply required, we may as well use the same one
            // item.AsDto() is our response payload object of the created item, converted to Dto
            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id}, item.AsDto());
        }

        [HttpPut("{id}")] // PUT /items/3fa85f64-5717-4562-b3fc-2c963f66afa6
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto) {
            var existingItem = await repository.GetItemAsync(id);

            if (existingItem is null) {
                return NotFound();
            }

            // Since itemDto is a record we can use 'with' to update whatever values that we provide/override
            Item updatedItem = existingItem with { 
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            await repository.UpdateItemAsync(updatedItem);

            return NoContent();
        }

        [HttpDelete("{id}")] // DELETE /items/3fa85f64-5717-4562-b3fc-2c963f66afa6
        public async Task<ActionResult> DeleteItemAsync(Guid id) {           
            var existingItem = await repository.GetItemAsync(id);

            
            if (existingItem is null) {
                return NotFound();
            }

            await repository.DeleteItemAsync(id);

            return NoContent();
        }
    }
}