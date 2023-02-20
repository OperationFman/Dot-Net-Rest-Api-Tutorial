using System;

namespace Catalog.Entities {
    public record Item { // Why record and not class? Records are immutable AND two of them with the exact same properties will evaluate true even if they're different instances
        public Guid Id { get; init; }
        public string Name {get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; } 
    }
}