using System.Collections.Generic;
using TIA.Core.DTOClasses;
using TIA.WebApp.Extentions;

namespace TIA.WebApp.Models
{
    public class ModalProductViewModel
    {
        public ProductDTO ProductDTO { get; set; }

        public ActionTypeEnum ActionType { get; set; }

        public List<CatalogDTO> CatalogList { get; set; }
    }
}
