using System;
using System.ComponentModel.DataAnnotations;

namespace TIA.DataAccessLayer.DTOClasses
{
    public abstract record CatalogObjectDTO : CoreObjectDTO
    {
        [Display(Name = "В каталоге")]
        public Guid? ParentCatalogId { get; init; }

        public CatalogDTO ParentCatalog { get; init; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Не указано название.")]
        public string Title { get; init; }

        [Display(Name = "Описание")]
        public string Description { get; init; }

        public bool IsActive { get; init; }

        public override string ToString() => this.Title;
    }
}
