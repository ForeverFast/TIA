using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TIA.Core.DTOClasses;
using TIA.Core.StoredProcedureModels;

namespace TIA.BusinessLogicBase.Abstractions
{
    public interface IProductModel
    {
        Task<ProductDTO> GetProductByIdAsync(Guid id);

        Task<List<ProductDataModel>> GetProductsFullDataAsync(DateTime? minDate = null, DateTime? maxDate = null, uint? minPrice = null, uint? maxPrice = null);

        Task<ProductDTO> AddProductAsync(ProductDTO productDTO);

        Task<ProductDTO> ChangeProductAsync(ProductDTO productDTO);

        Task<bool> DeleteProductAsync(Guid id);

        Task<bool> SafeDeleteProductAsync(Guid id);
    }
}
