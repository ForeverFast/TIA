using System;
using System.Collections.Generic;

#nullable disable

namespace TIA.DataAccessLayer.Models
{
    public partial class Product
    {
        public Guid Id { get; set; }
        public long Quantity { get; set; }
        public long Price { get; set; }
        public Guid? ParentCatalogId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime? SomeDate { get; set; }

        public virtual Catalog ParentCatalog { get; set; }
    }
}
