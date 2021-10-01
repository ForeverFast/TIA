using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TIA.DataAccessLayer.Models
{
    public partial class Catalog
    {
        public Catalog()
        {
            InverseParentCatalog = new HashSet<Catalog>();
            Products = new HashSet<Product>();
        }

        public Guid Id { get; set; }
        public Guid? ParentCatalogId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public virtual Catalog ParentCatalog { get; set; }
        public virtual ICollection<Catalog> InverseParentCatalog { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
