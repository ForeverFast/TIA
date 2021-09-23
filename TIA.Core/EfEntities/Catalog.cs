using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TIA.Core.EfEntities
{
    [Table("Catalogs")]
    public class Catalog : CatalogObject
    {
        public List<Catalog> Catalogs { get; set; }

        public List<Product> Products { get; set; }

        public Catalog()
        {
            Catalogs = new List<Catalog>();
            Products = new List<Product>();
        }
    }
}
