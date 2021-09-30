﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TIA.BusinessLogicLayerBase.Abstractions;
using TIA.DataAccessLayer.DTOClasses;

namespace TIA.BusinessLogicLayerBase
{
    public abstract partial class TiaModelBase : ITiaModel, ICatalogModel
    {
        public Task<CatalogDTO> GetCatalogByIdAsync(Guid id)
            => Task.Run(() => GetCatalogByIdExecute(id));
        protected abstract CatalogDTO GetCatalogByIdExecute(Guid id);

        public Task<List<ProductDTO>> GetCatalogProductsWithFiltersAsync(Guid id, string title = "", DateTime? minDate = null, DateTime? maxDate = null, uint? minPrice = null, uint? maxPrice = null)
            => Task.Run(() => GetCatalogProductsWithFiltersExecute(id, title, minDate, maxDate, minPrice, maxPrice));
        protected abstract List<ProductDTO> GetCatalogProductsWithFiltersExecute(Guid id, string title, DateTime? minDate, DateTime? maxDate, uint? minPrice, uint? maxPrice);

        public Task<List<CatalogDTO>> GetCatalogsTreeAsync()
            => Task.Run(() => GetCatalogsTreeExecute());
        protected abstract List<CatalogDTO> GetCatalogsTreeExecute();

        public Task<List<CatalogDTO>> GetCatalogsLineCollectionAsync() 
            => Task.Run(() => GetCatalogsLineCollectionExecute());
        protected abstract List<CatalogDTO> GetCatalogsLineCollectionExecute();

        public Task<CatalogDTO> AddCatalogAsync(CatalogDTO catalogDTO)
            => Task.Run(() => AddCatalogExecute(catalogDTO));
        protected abstract CatalogDTO AddCatalogExecute(CatalogDTO catalogDTO);

        public Task<CatalogDTO> ChangeCatalogAsync(CatalogDTO catalogDTO)
            => Task.Run(() => ChangeCatalogExecute(catalogDTO));
        protected abstract CatalogDTO ChangeCatalogExecute(CatalogDTO catalogDTO);

        public Task<bool> DeleteCatalogAsync(Guid id)
            => Task.Run(() => DeleteCatalogExecute(id));
        protected abstract bool DeleteCatalogExecute(Guid id);
    }
}
