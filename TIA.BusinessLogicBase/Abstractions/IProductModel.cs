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

        Task<bool> DeleteProductAsync(ProductDTO productDTO);

        Task<bool> SafeDeleteProductAsync(ProductDTO productDTO);
    }
}
