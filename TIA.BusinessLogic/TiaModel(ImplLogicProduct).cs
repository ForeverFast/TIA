using System;
using TIA.BusinessLogicBase;
using TIA.Core.DTOClasses;

namespace TIA.BusinessLogic
{
    public partial class TiaModel : TiaModelBase
    {
        protected override ProductDTO GetProductById(Guid id)
        {
            ProductDTO productDTO = productDataService.GetById(id).Result;
            return productDTO;
        }

        protected override ProductDTO AddProduct(ProductDTO productDTO)
        {
            if (string.IsNullOrEmpty(productDTO.Title))
                throw new ArgumentNullException(nameof(productDTO.Title), "У товара должно быть название.");

            if (productDTO.Price == 0)
                throw new ArgumentNullException(nameof(productDTO.Price), "Товар не может быть бесплатным");

            ProductDTO dbCreatedProduct = productDataService.Add(productDTO).Result;
            return dbCreatedProduct;
        }


        protected override ProductDTO ChangeProduct(ProductDTO productDTO)
        {
            if (productDTO.Id == Guid.Empty)
                throw new ArgumentNullException(nameof(productDTO.Id), "Для изменение каталога нужен его Id.");

            if (string.IsNullOrEmpty(productDTO.Title))
                throw new ArgumentNullException(nameof(productDTO.Title), "У товара должно быть название.");

            if (productDTO.Price == 0)
                throw new ArgumentNullException(nameof(productDTO.Price), "Товар не может быть бесплатным");

            ProductDTO dbChangedProduct = productDataService.Update(productDTO, productDTO.Id).Result;
            return dbChangedProduct;
        }


        protected override bool DeleteProduct(ProductDTO productDTO)
        {
            if (productDTO.Id == Guid.Empty)
                throw new ArgumentNullException(nameof(productDTO.Id), "Для удаления товара нужен его Id.");

            bool operationResult = productDataService.Delete(productDTO.Id).Result;
            return operationResult;
        }

        protected override bool SafeDeleteProduct(ProductDTO productDTO)
        {
            if (productDTO.Id == Guid.Empty)
                throw new ArgumentNullException(nameof(productDTO.Id), "Для скрытия товара нужен его Id.");

            bool operationResult = productDataService.SafeDelete(productDTO.Id).Result;
            return operationResult;
        }
    }
}
