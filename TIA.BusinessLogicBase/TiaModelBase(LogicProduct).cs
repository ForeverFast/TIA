using System;
using System.Threading.Tasks;
using TIA.BusinessLogicBase.Abstractions;
using TIA.Core.DTOClasses;

namespace TIA.BusinessLogicBase
{
    public abstract partial class TiaModelBase : ITiaModel, IProductModel
    {
        public Task<ProductDTO> GetProductByIdAsync(Guid id) => Task.Factory.StartNew(() => GetProductById(id));
        protected abstract ProductDTO GetProductById(Guid id);

        public Task<ProductDTO> AddProductAsync(ProductDTO productDTO) => Task.Factory.StartNew(() => AddProduct(productDTO));
        protected abstract ProductDTO AddProduct(ProductDTO productDTO);

        public Task<ProductDTO> ChangeProductAsync(ProductDTO productDTO) => Task.Factory.StartNew(() => ChangeProduct(productDTO));
        protected abstract ProductDTO ChangeProduct(ProductDTO productDTO);

        public Task<bool> DeleteProductAsync(ProductDTO productDTO) => Task.Factory.StartNew(() => DeleteProduct(productDTO));
        protected abstract bool DeleteProduct(ProductDTO productDTO);

        public Task<bool> SafeDeleteProductAsync(ProductDTO productDTO) => Task.Factory.StartNew(() => SafeDeleteProduct(productDTO));
        protected abstract bool SafeDeleteProduct(ProductDTO productDTO);
    }
}
