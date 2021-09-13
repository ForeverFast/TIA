using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TIA.Core.EfEntities
{
    public abstract class CatalogObject : CoreObject
    {
        public Guid? ParentCatalogId { get; set; }

        [ForeignKey("ParentCatalogId")]
        public Catalog ParentCatalog { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}
