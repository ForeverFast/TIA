using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TIA.BusinessLogicBase.Abstractions;
using TIA.Core.DTOClasses;

namespace TIA.BusinessLogicBase
{
    public abstract partial class TiaModelBase : ITiaModel, ICatalogModel
    {
        public Task<CatalogDTO> GetCatalogByIdAsync(Guid id) => Task.Factory.StartNew(() => GetCatalogById(id));
        protected abstract CatalogDTO GetCatalogById(Guid id);

        public Task<List<CatalogDTO>> GetCatalogsTreeAsync() => Task.Factory.StartNew(() => GetCatalogsTree());
        protected abstract List<CatalogDTO> GetCatalogsTree();

        public Task<CatalogDTO> AddCatalogAsync(CatalogDTO catalogDTO) => Task.Factory.StartNew(() => AddCatalog(catalogDTO));
        protected abstract CatalogDTO AddCatalog(CatalogDTO catalogDTO);

        public Task<CatalogDTO> ChangeCatalogAsync(CatalogDTO catalogDTO) => Task.Factory.StartNew(() => ChangeCatalog(catalogDTO));
        protected abstract CatalogDTO ChangeCatalog(CatalogDTO catalogDTO);

        public Task<bool> DeleteCatalogAsync(CatalogDTO catalogDTO) => Task.Factory.StartNew(() => DeleteCatalog(catalogDTO));
        protected abstract bool DeleteCatalog(CatalogDTO catalogDTO);
    }
}
