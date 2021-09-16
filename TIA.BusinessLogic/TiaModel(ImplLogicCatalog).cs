using System;
using System.Collections.Generic;
using System.Linq;
using TIA.BusinessLogicBase;
using TIA.Core.DTOClasses;

namespace TIA.BusinessLogic
{
    public partial class TiaModel : TiaModelBase
    {
        protected override CatalogDTO GetCatalogById(Guid id)
        {
            CatalogDTO catalogDTO = catalogDataService.GetById(id).Result;
            return catalogDTO;
        }

        protected override List<CatalogDTO> GetCatalogsTree()
        {
            List<CatalogDTO> tree = catalogDataService.GetCatalogsTree().Result.ToList();
            return tree;
        }

        protected override CatalogDTO AddCatalog(CatalogDTO catalogDTO)
        {
            if (string.IsNullOrEmpty(catalogDTO.Title))
                throw new ArgumentNullException(nameof(catalogDTO.Title), "У каталога должно быть название.");

            CatalogDTO dbCreatedCatalog = catalogDataService.Add(catalogDTO).Result;
            return dbCreatedCatalog;
        }

        protected override CatalogDTO ChangeCatalog(CatalogDTO catalogDTO)
        {
            if (catalogDTO.Id == Guid.Empty)
                throw new ArgumentNullException(nameof(catalogDTO.Id), "Для изменение каталога нужен его Id.");

            if (string.IsNullOrEmpty(catalogDTO.Title))
                throw new ArgumentNullException(nameof(catalogDTO.Title), "У каталога должно быть название.");

            CatalogDTO dbChangedCatalog = catalogDataService.Update(catalogDTO, catalogDTO.Id).Result;
            return dbChangedCatalog;
        }

        protected override bool DeleteCatalog(CatalogDTO catalogDTO)
        {
            if (catalogDTO.Id == Guid.Empty)
                throw new ArgumentNullException(nameof(catalogDTO.Id), "Для скрытия каталога нужен его Id.");

            bool operationResult = catalogDataService.Delete(catalogDTO.Id).Result;
            return operationResult;
        }     
    }
}
