using System;
using System.ComponentModel.DataAnnotations;

namespace TIA.RestAPI.Models
{
    public class InputProductData
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Не указана каталог товара")]
        public Guid? ParentCatalogId { get; set; }

        [Required(ErrorMessage = "Не указано название.")]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Не указана цена товара")]
        [Range(1, int.MaxValue, ErrorMessage = "Недопустимое значение")]
        public uint Price { get; set; }

        public override string ToString() => this.Title;
    }
}
