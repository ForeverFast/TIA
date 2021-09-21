using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TIA.Core.DTOClasses;
using TIA.WebApp.Extentions;

namespace TIA.WebApp.Models
{
    public class ModalProductViewModel
    {
        public ActionTypeEnum ActionType { get; set; }

        public List<CatalogDTO> CatalogList { get; set; }

        public bool IsEmptyCatalog { get; set; }



        public Guid Id { get; set; }

        public Guid? ParentCatalogId { get; set; }

        [Required(ErrorMessage = "Не указано название.")]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Недопустимый возраст")]
        public uint Price { get; set; }
    }
}
