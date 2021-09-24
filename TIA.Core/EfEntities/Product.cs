using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TIA.Core.EfEntities
{
    [Table("Products")]
    public class Product : CatalogObject
    {
        public DateTime? SomeDate { get; set; }
        public uint Quantity { get; set; }
        public uint Price { get; set; }
    }
}
