using System;
using System.Collections.Generic;
using TIA.BusinessLogicLayerBase;
using TIA.DataAccessLayer.DTOClasses;

namespace TIA.BusinessLogicLayer
{
    public partial class TiaModel : TiaModelBase
    {
        protected override CatalogDTO GetCatalogByIdExecute(Guid id)
        {
            CatalogDTO catalog = catalogRepository.GetById(id);
            return catalog;
        }
         
        protected override List<ProductDTO> GetCatalogProductsWithFiltersExecute(Guid id, string title, DateTime? minDate, DateTime? maxDate, uint? minPrice, uint? maxPrice)
        {
            List<ProductDTO> products = catalogRepository.GetCatalogProductsWithFilters(id, title, minDate, maxDate, minPrice, maxPrice);
            return products;
        }

        protected override List<CatalogDTO> GetCatalogsTreeExecute()
        {
            List<CatalogDTO> tree = catalogRepository.GetCatalogsTree();
            return tree;
        }

        protected override List<CatalogDTO> GetCatalogsLineCollectionExecute()
        {
            List<CatalogDTO> tree = catalogRepository.GetCatalogsLineCollection();
            return tree;
        }

        protected override CatalogDTO AddCatalogExecute(CatalogDTO catalogDTO)
        {
            if (string.IsNullOrEmpty(catalogDTO.Title))
                throw new ArgumentNullException(nameof(catalogDTO.Title), "У каталога должно быть название.");

            CatalogDTO dbCreatedCatalog = catalogRepository.Add(catalogDTO);
            return dbCreatedCatalog;
        }

        protected override CatalogDTO ChangeCatalogExecute(CatalogDTO catalogDTO)
        {
            if (catalogDTO.Id == Guid.Empty)
                throw new ArgumentNullException(nameof(catalogDTO.Id), "Для изменение каталога нужен его Id.");

            if (string.IsNullOrEmpty(catalogDTO.Title))
                throw new ArgumentNullException(nameof(catalogDTO.Title), "У каталога должно быть название.");

            CatalogDTO dbChangedCatalog = catalogRepository.Update(catalogDTO, catalogDTO.Id);
            return dbChangedCatalog;
        }

        protected override bool DeleteCatalogExecute(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id), "Для скрытия каталога нужен его Id.");

            CatalogDTO catalog = catalogRepository.GetById(id);
            if (catalog.Products.Count > 0 || catalog.Catalogs.Count > 0)
                return false;

            bool operationResult =  catalogRepository.Delete(id);
            return operationResult;
        }     
    }
}
