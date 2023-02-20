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
        public IEnumerable<ItemDto> GetItems() {
            var items = repository.GetItems().Select(item => item.AsDto());
            return items;
        }

        [HttpGet("{id}")] // GET /items/3fa85f64-5717-4562-b3fc-2c963f66afa6
        public ActionResult<ItemDto> GetItem(Guid id) { // Add ActionResult to allows us to return various status codes depending on errors
            var item = repository.GetItem(id);

            if (item is null) {
                return NotFound(); // Returns "404 not found" for us
            }

            return item.AsDto();
        }

        [HttpPost] // POST
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto) {
            Item item = new(){ 
                Id = Guid.NewGuid(), // generate ID
                Name = itemDto.Name, 
                Price = itemDto.Price, 
                CreatedDate = DateTimeOffset.UtcNow // generate Date
            };

            // Save the new resource
            repository.CreateItem(item);

            // It's convention to return the created resource
            // CreatedAtAction adds "201 Created"
            // nameof is just a title, we could add anything
            // A new id is simply required, we may as well use the same one
            // item.AsDto() is our response payload object of the created item, converted to Dto
            return CreatedAtAction(nameof(GetItem), new { id = item.Id}, item.AsDto());
        }

        [HttpPut("{id}")] // PUT /items/3fa85f64-5717-4562-b3fc-2c963f66afa6
        public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto) {
            var existingItem = repository.GetItem(id);

            if (existingItem is null) {
                return NotFound();
            }

            // Since itemDto is a record we can use 'with' to update whatever values that we provide/override
            Item updatedItem = existingItem with { 
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            repository.UpdateItem(updatedItem);

            return NoContent();
        }

        [HttpDelete("{id}")] // DELETE /items/3fa85f64-5717-4562-b3fc-2c963f66afa6
        public ActionResult DeleteItem(Guid id, UpdateItemDto itemDto) {
            var existingItem = repository.GetItem(id);

            if (existingItem is null) {
                return NotFound();
            }

            repository.DeleteItem(id);

            return NoContent();
        }
    }
}