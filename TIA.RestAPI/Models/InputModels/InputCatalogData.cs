using System;
using System.ComponentModel.DataAnnotations;

namespace TIA.RestAPI.Models
{
    public class InputCatalogData 
    {
        public Guid Id { get; set; }

        public Guid? ParentCatalogId { get; set; }

        [Required(ErrorMessage = "Не указано название.")]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public override string ToString() => this.Title;
    }
}
