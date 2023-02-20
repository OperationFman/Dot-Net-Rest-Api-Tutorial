using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers {
    [ApiController] // Adds some automatic functionality for us like errors and routing, it's called an attribute
    [Route("items")] // Automatically is: /items
    public class ItemsController : ControllerBase { // ControllerBase converts this into a controller class, ez
        private readonly InMemItemsRepository repository; 

        public ItemsController() {
            repository = new InMemItemsRepository();
        }

        [HttpGet] // GET /items, will call this method
        public IEnumerable<Item> GetItems() {
            var items = repository.GetItems();
            return items;
        }

        [HttpGet("{id}")]
        public ActionResult<Item> GetItem(Guid id) { // Add ActionResult to allows us to return various status codes depending on errors
            var item = repository.GetItem(id);

            if (item is null) {
                return NotFound(); // Returns 404 not found for us
            }

            return item;
        }
    }
}