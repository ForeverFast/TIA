using System.Collections.Generic;
using TIA.Core.DTOClasses;

namespace TIA.WebApp.Models
{
    public class ModalCatalogViewModel
    {
        public CatalogDTO CatalogDTO { get; set; }
        public List<CatalogDTO> CatalogList { get; set; }
    }
}
