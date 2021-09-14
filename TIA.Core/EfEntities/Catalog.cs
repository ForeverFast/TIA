using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TIA.Core.EfEntities
{
    [Table("Catalogs")]
    public class Catalog : CatalogObject
    {
        public IEnumerable<Catalog> Catalogs { get; set; }

        public IEnumerable<Product> Products { get; set; }

        public Catalog()
        {
            Catalogs = new List<Catalog>();
            Products = new List<Product>();
        }
    }
}
