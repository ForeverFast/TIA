using System;

namespace TIA.Core.DTOClasses
{
    public abstract record CatalogObjectDTO : CoreObjectDTO
    {
        public Guid? ParentCatalogId { get; init; }

        public CatalogDTO ParentCatalog { get; init; } 
        
        public string Title { get; init; }

        public string Description { get; init; }

        public bool IsActive { get; init; }
    }
}
