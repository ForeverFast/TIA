using System.Collections.Generic;
using TIA.DataAccessLayer.DTOClasses;

namespace TIA.WebInterface.Models
{
    public class ModalCatalogViewModel
    {
        public CatalogDTO CatalogDTO { get; set; }
        public List<CatalogDTO> CatalogList { get; set; }
    }
}
