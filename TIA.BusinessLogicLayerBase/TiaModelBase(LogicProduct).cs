using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TIA.BusinessLogicLayerBase.Abstractions;
using TIA.DataAccessLayer.DTOClasses;
using TIA.DataAccessLayer.Models;

namespace TIA.BusinessLogicLayerBase
{
    public abstract partial class TiaModelBase : ITiaModel, IProductModel
    {
        public Task<ProductDTO> GetProductByIdAsync(Guid id)
            => Task.Run(() => GetProductByIdExecute(id));
        protected abstract ProductDTO GetProductByIdExecute(Guid id);

        public Task<List<ProductDataModel>> GetProductsFullDataAsync(DateTime? minDate = null, DateTime? maxDate = null, uint? minPrice = null, uint? maxPrice = null)
            => Task.Run(() => GetProductsFullData(minDate, maxDate, minPrice, maxPrice));
        protected abstract List<ProductDataModel> GetProductsFullData(DateTime? minDate, DateTime? maxDate, uint? minPrice, uint? maxPrice);

        public Task<ProductDTO> AddProductAsync(ProductDTO productDTO)
            => Task.Run(() => AddProductExecute(productDTO));
        protected abstract ProductDTO AddProductExecute(ProductDTO productDTO);

        public Task<ProductDTO> ChangeProductAsync(ProductDTO productDTO)
            => Task.Run(() => ChangeProductExecute(productDTO));
        protected abstract ProductDTO ChangeProductExecute(ProductDTO productDTO);

        public Task<bool> DeleteProductAsync(Guid id)
            => Task.Run(() => DeleteProductExecute(id));
        protected abstract bool DeleteProductExecute(Guid id);

        public Task<bool> SafeDeleteProductAsync(Guid id) 
            => Task.Run(() => SafeDeleteProductExecute(id));
        protected abstract bool SafeDeleteProductExecute(Guid id);
    }
}
