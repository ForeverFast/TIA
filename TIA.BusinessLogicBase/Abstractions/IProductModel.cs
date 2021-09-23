using System;
using System.Threading.Tasks;
using TIA.Core.DTOClasses;

namespace TIA.BusinessLogicBase.Abstractions
{
    public interface IProductModel
    {
        Task<ProductDTO> GetProductByIdAsync(Guid id);

        Task<ProductDTO> AddProductAsync(ProductDTO productDTO);

        Task<ProductDTO> ChangeProductAsync(ProductDTO productDTO);

        Task<bool> DeleteProductAsync(Guid id);

        Task<bool> SafeDeleteProductAsync(Guid id);
    }
}
