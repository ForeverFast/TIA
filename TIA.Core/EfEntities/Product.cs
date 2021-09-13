using System.ComponentModel.DataAnnotations.Schema;

namespace TIA.Core.EfEntities
{
    [Table("Products")]
    public class Product : CatalogObject
    {
        public uint Quantity { get; set; }
        public uint Price { get; set; }
    }
}
