using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TIA.DataAccessLayer.DTOClasses;
using TIA.DataAccessLayer.Models;

namespace TIA.BusinessLogicLayerBase.Abstractions
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
