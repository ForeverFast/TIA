using System;

namespace TIA.DataAccessLayer.DTOClasses
{
    public record ProductDTO : CatalogObjectDTO
    {
        public DateTime? SomeDate { get; set; }

        public long Quantity { get; init; }

        public long Price { get; init; }
    }
}
