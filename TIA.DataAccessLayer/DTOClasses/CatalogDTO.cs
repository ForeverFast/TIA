using System.Collections.Generic;

namespace TIA.DataAccessLayer.DTOClasses
{
    public record CatalogDTO : CatalogObjectDTO
    {
        public List<CatalogDTO> Catalogs { get; init; }

        public List<ProductDTO> Products { get; init; }
    }
}
