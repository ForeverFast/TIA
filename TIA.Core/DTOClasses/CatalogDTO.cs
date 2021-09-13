using System.Collections.Generic;

namespace TIA.Core.DTOClasses
{
    public record CatalogDTO : CatalogObjectDTO
    {
        public IEnumerable<CatalogDTO> Catalogs { get; init; }

        public IEnumerable<ProductDTO> Products { get; init; }
    }
}
