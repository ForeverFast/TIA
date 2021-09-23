using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TIA.BusinessLogicBase.Abstractions;
using TIA.Core.DTOClasses;

namespace TIA.BusinessLogicBase
{
    public abstract partial class TiaModelBase : ITiaModel, ICatalogModel
    {
        public Task<CatalogDTO> GetCatalogByIdAsync(Guid id) => Task.Run(() => GetCatalogByIdExecute(id));
        protected abstract Task<CatalogDTO> GetCatalogByIdExecute(Guid id);

        public Task<List<CatalogDTO>> GetCatalogsTreeAsync() => Task.Run(() => GetCatalogsTreeExecute());
        protected abstract Task<List<CatalogDTO>> GetCatalogsTreeExecute();

        public Task<List<CatalogDTO>> GetCatalogsLineCollectionAsync() => Task.Run(() => GetCatalogsLineCollectionExecute());
        protected abstract Task<List<CatalogDTO>> GetCatalogsLineCollectionExecute();

        public Task<CatalogDTO> AddCatalogAsync(CatalogDTO catalogDTO) => Task.Run(() => AddCatalogExecute(catalogDTO));
        protected abstract Task<CatalogDTO> AddCatalogExecute(CatalogDTO catalogDTO);

        public Task<CatalogDTO> ChangeCatalogAsync(CatalogDTO catalogDTO) => Task.Run(() => ChangeCatalogExecute(catalogDTO));
        protected abstract Task<CatalogDTO> ChangeCatalogExecute(CatalogDTO catalogDTO);

        public Task<bool> DeleteCatalogAsync(Guid id) => Task.Run(() => DeleteCatalogExecute(id));
        protected abstract Task<bool> DeleteCatalogExecute(Guid id);
    }
}
