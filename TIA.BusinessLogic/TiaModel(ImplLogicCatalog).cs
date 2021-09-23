using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TIA.BusinessLogicBase;
using TIA.Core.DTOClasses;

namespace TIA.BusinessLogic
{
    public partial class TiaModel : TiaModelBase
    {
        protected override async Task<CatalogDTO> GetCatalogByIdExecute(Guid id)
        {
            CatalogDTO catalog = await catalogDataService.GetById(id);
            return catalog;
        }

        protected override async Task<List<CatalogDTO>> GetCatalogsTreeExecute()
        {
            List<CatalogDTO> tree = await catalogDataService.GetCatalogsTree();
            return tree;
        }

        protected override async Task<List<CatalogDTO>> GetCatalogsLineCollectionExecute()
        {
            List<CatalogDTO> tree = await catalogDataService.GetCatalogsLineCollection();
            return tree;
        }

        protected override async Task<CatalogDTO> AddCatalogExecute(CatalogDTO catalogDTO)
        {
            if (string.IsNullOrEmpty(catalogDTO.Title))
                throw new ArgumentNullException(nameof(catalogDTO.Title), "У каталога должно быть название.");

            CatalogDTO dbCreatedCatalog = await catalogDataService.Add(catalogDTO);
            return dbCreatedCatalog;
        }

        protected override async Task<CatalogDTO> ChangeCatalogExecute(CatalogDTO catalogDTO)
        {
            if (catalogDTO.Id == Guid.Empty)
                throw new ArgumentNullException(nameof(catalogDTO.Id), "Для изменение каталога нужен его Id.");

            if (string.IsNullOrEmpty(catalogDTO.Title))
                throw new ArgumentNullException(nameof(catalogDTO.Title), "У каталога должно быть название.");

            CatalogDTO dbChangedCatalog = await catalogDataService.Update(catalogDTO, catalogDTO.Id);
            return dbChangedCatalog;
        }

        protected override async Task<bool> DeleteCatalogExecute(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id), "Для скрытия каталога нужен его Id.");

            CatalogDTO catalog = await catalogDataService.GetById(id);
            if (catalog.Products.Count > 0 || catalog.Catalogs.Count > 0)
                return false;

            bool operationResult = await catalogDataService.Delete(id);
            return operationResult;
        }     
    }
}
