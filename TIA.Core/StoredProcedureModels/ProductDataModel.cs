using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIA.Core.StoredProcedureModels
{
    [Keyless]
    public class ProductDataModel
    {
        
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? SomeDate { get; set; }
        public uint Quantity { get; set; }
        public uint Price { get; set; }


        public Guid? ParentCatalogId { get; set; }
        public string ParentCatalogTitle { get; set; }
        public Guid? ParentParentCatalogId { get; set; }
        public string ParentParentCatalogTitle { get; set; }
    }
}
