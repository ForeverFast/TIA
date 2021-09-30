using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TIA.BusinessLogicBase;
using TIA.Core.DTOClasses;
using TIA.Core.StoredProcedureModels;

namespace TIA.BusinessLogic
{
    public partial class TiaModel : TiaModelBase
    {
        protected override async Task<ProductDTO> GetProductByIdExecute(Guid id)
        {
            ProductDTO productDTO = await productDataService.GetById(id);
            return productDTO;
        }

        protected override List<ProductDataModel> GetProductsFullData(DateTime? minDate, DateTime? maxDate, uint? minPrice, uint? maxPrice)
        {
            List<ProductDataModel> dataModels = productDataService.GetProductsFullData(minDate, maxDate, minPrice, maxPrice);
            return dataModels;
        }

        protected override async Task<ProductDTO> AddProductExecute(ProductDTO productDTO)
        {
            if (string.IsNullOrEmpty(productDTO.Title))
                throw new ArgumentNullException(nameof(productDTO.Title), "У товара должно быть название.");

            if (productDTO.Price == 0)
                throw new ArgumentNullException(nameof(productDTO.Price), "Товар не может быть бесплатным");

            ProductDTO dbCreatedProduct = await productDataService.Add(productDTO);
            return dbCreatedProduct;
        }


        protected override async Task<ProductDTO> ChangeProductExecute(ProductDTO productDTO)
        {
            if (productDTO.Id == Guid.Empty)
                throw new ArgumentNullException(nameof(productDTO.Id), "Для изменение каталога нужен его Id.");

            if (string.IsNullOrEmpty(productDTO.Title))
                throw new ArgumentNullException(nameof(productDTO.Title), "У товара должно быть название.");

            if (productDTO.Price == 0)
                throw new ArgumentNullException(nameof(productDTO.Price), "Товар не может быть бесплатным");

            ProductDTO dbChangedProduct = await productDataService.Update(productDTO, productDTO.Id);
            return dbChangedProduct;
        }


        protected override async Task<bool> DeleteProductExecute(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id), "Для удаления товара нужен его Id.");

            bool operationResult = await productDataService.Delete(id);
            return operationResult;
        }

        protected override async Task<bool> SafeDeleteProductExecute(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id), "Для скрытия товара нужен его Id.");

            bool operationResult = await productDataService.SafeDelete(id);
            return operationResult;
        }
    }
}
