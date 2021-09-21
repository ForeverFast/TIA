using System;
using System.ComponentModel.DataAnnotations;

namespace TIA.Core.DTOClasses
{
    public abstract record CatalogObjectDTO : CoreObjectDTO
    {
        public Guid? ParentCatalogId { get; init; }

        public CatalogDTO ParentCatalog { get; init; }

        [Required(ErrorMessage = "Не указано название.")]
        public string Title { get; init; }

        public string Description { get; init; }

        public bool IsActive { get; init; }

        public override string ToString() => this.Title;
    }
}
