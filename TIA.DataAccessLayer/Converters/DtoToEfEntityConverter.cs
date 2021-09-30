﻿using System.Linq;
using TIA.DataAccessLayer.DTOClasses;
using TIA.DataAccessLayer.Models;

namespace TIA.DataAccessLayer.Converters
{
    public static class DtoToEfEntityConverter
    {
        public static Product ConvertProductDTO(this ProductDTO productDTO)
           => productDTO == null ? null : new Product
           {
               Id = productDTO.Id,
               ParentCatalogId = productDTO.ParentCatalogId,
               Title = productDTO.Title,
               Description = productDTO.Description,
               SomeDate = productDTO.SomeDate,
               Price = productDTO.Price,
               Quantity = productDTO.Quantity,
               IsActive = productDTO.IsActive
           };

        public static Catalog ConvertCatalogDTO(this CatalogDTO catalogDTO)
            => catalogDTO == null ? null : new Catalog
            {
                Id = catalogDTO.Id,
                ParentCatalogId = catalogDTO.ParentCatalogId,
                Title = catalogDTO.Title,
                Description = catalogDTO.Description,
                Products = catalogDTO.Products?.Select(p => p.ConvertProductDTO()).ToList(),
                InverseParentCatalog = catalogDTO.Catalogs?.Select(p => p.ConvertCatalogDTO()).ToList(),
                IsActive = catalogDTO.IsActive
            };
    }
}
