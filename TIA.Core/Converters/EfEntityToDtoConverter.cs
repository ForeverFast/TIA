using System.Linq;
using TIA.Core.DTOClasses;
using TIA.Core.EfEntities;

namespace TIA.Core.Converters
{
    public static class EfEntityToDtoConverter
    {
        public static ProductDTO ConvertProduct(this Product product)
            => new ProductDTO
            {
                Id = product.Id,
                ParentCatalogId = product.ParentCatalogId,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                IsActive = product.IsActive
            };

        public static CatalogDTO ConvertCatalog(this Catalog catalog)
            => new CatalogDTO
            {
                Id = catalog.Id,
                ParentCatalogId = catalog.ParentCatalogId,
                Title = catalog.Title,
                Description = catalog.Description,
                Products = catalog.Products?.Select(p => p.ConvertProduct()),
                Catalogs = catalog.Catalogs?.Select(p => p.ConvertCatalog()),
                IsActive = catalog.IsActive
            };
    }
}
