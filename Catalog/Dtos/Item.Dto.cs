using System;

namespace Catalog.Dto { // Added Dto
    public record ItemDto { // Added Dto
        public Guid Id { get; init; }
        public string Name {get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; } 
    }
}