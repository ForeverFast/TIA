﻿using System;
using System.Collections.Generic;

#nullable disable

namespace TIA.DataAccessLayer.Models
{
    public partial class ProductDataModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? SomeDate { get; set; }
        public long Quantity { get; set; }
        public long Price { get; set; }
        public Guid ParentCatalogId { get; set; }
        public string ParentCatalogTitle { get; set; }
        public Guid ParentParentCatalogId { get; set; }
        public string ParentParentCatalogTitle { get; set; }
    }
}
