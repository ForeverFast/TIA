using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TIA.DataAccessLayer.DTOClasses;

namespace TIA.WebInterface.Models
{
    public class ModalProductViewModel
    {
        public List<CatalogDTO> CatalogList { get; set; }

        public bool IsEmptyCatalog { get; set; }



        public Guid Id { get; set; }

        [Display(Name = "В каталоге")]
        public Guid? ParentCatalogId { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Не указано название.")]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        public bool IsActive { get; set; }

        [Display(Name = "Цена")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Недопустимое значение цены")]
        public long Price { get; set; }
    }
}
