using System;
using System.Threading.Tasks;
using TIA.BusinessLogicBase.Abstractions;
using TIA.Core.DTOClasses;

namespace TIA.BusinessLogicBase
{
    public abstract partial class TiaModelBase : ITiaModel, IProductModel
    {
        public Task<ProductDTO> GetProductByIdAsync(Guid id) => Task.Run(() => GetProductByIdExecute(id));
        protected abstract Task<ProductDTO> GetProductByIdExecute(Guid id);

        public Task<ProductDTO> AddProductAsync(ProductDTO productDTO) => Task.Run(() => AddProductExecute(productDTO));
        protected abstract Task<ProductDTO> AddProductExecute(ProductDTO productDTO);

        public Task<ProductDTO> ChangeProductAsync(ProductDTO productDTO) => Task.Run(() => ChangeProductExecute(productDTO));
        protected abstract Task<ProductDTO> ChangeProductExecute(ProductDTO productDTO);

        public Task<bool> DeleteProductAsync(Guid id) => Task.Run(() => DeleteProductExecute(id));
        protected abstract Task<bool> DeleteProductExecute(Guid id);

        public Task<bool> SafeDeleteProductAsync(Guid id) => Task.Run(() => SafeDeleteProductExecute(id));
        protected abstract Task<bool> SafeDeleteProductExecute(Guid id);
    }
}
