using Catalog.Dto;
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

        [HttpGet] // GET /items, will call this method
        public IEnumerable<ItemDto> GetItems() {
            var items = repository.GetItems().Select(item => item.AsDto());
            return items;
        }

        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id) { // Add ActionResult to allows us to return various status codes depending on errors
            var item = repository.GetItem(id);

            if (item is null) {
                return NotFound(); // Returns 404 not found for us
            }

            return item.AsDto();
        }
    }
}