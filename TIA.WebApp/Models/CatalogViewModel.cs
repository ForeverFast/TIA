using System.Collections.Generic;
using TIA.Core.DTOClasses;

namespace TIA.WebApp.Models
{
    public class CatalogViewModel
    {
        public IEnumerable<CatalogDTO> CatalogTree { get; set; }
        public CatalogDTO Catalog { get; set; }
    }
}
