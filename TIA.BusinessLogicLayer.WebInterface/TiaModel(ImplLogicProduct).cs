using System;
using System.Collections.Generic;
using TIA.BusinessLogicLayerBase;
using TIA.DataAccessLayer.DTOClasses;
using TIA.DataAccessLayer.Models;

namespace TIA.BusinessLogicLayer.WebInterface
{
    public partial class TiaModel : TiaModelBase
    {
        protected override ProductDTO GetProductByIdExecute(Guid id)
        {
            //ProductDTO productDTO = productRepository.GetById(id);
            //return productDTO;
            throw new NotImplementedException();
        }

        protected override List<ProductDataModel> GetProductsFullData(DateTime? minDate, DateTime? maxDate, uint? minPrice, uint? maxPrice)
        {
            //List<ProductDataModel> dataModels = productRepository.GetProductsFullData(minDate, maxDate, minPrice, maxPrice);
            //return dataModels;
            throw new NotImplementedException();
        }

        protected override ProductDTO AddProductExecute(ProductDTO productDTO)
        {
            //if (string.IsNullOrEmpty(productDTO.Title))
            //    throw new ArgumentNullException(nameof(productDTO.Title), "У товара должно быть название.");

            //if (productDTO.Price == 0)
            //    throw new ArgumentNullException(nameof(productDTO.Price), "Товар не может быть бесплатным");

            //ProductDTO dbCreatedProduct = productRepository.Add(productDTO);
            //return dbCreatedProduct;
            throw new NotImplementedException();
        }


        protected override ProductDTO ChangeProductExecute(ProductDTO productDTO)
        {
            //if (productDTO.Id == Guid.Empty)
            //    throw new ArgumentNullException(nameof(productDTO.Id), "Для изменение каталога нужен его Id.");

            //if (string.IsNullOrEmpty(productDTO.Title))
            //    throw new ArgumentNullException(nameof(productDTO.Title), "У товара должно быть название.");

            //if (productDTO.Price == 0)
            //    throw new ArgumentNullException(nameof(productDTO.Price), "Товар не может быть бесплатным");

            //ProductDTO dbChangedProduct = productRepository.Update(productDTO, productDTO.Id);
            //return dbChangedProduct;
            throw new NotImplementedException();
        }


        protected override bool DeleteProductExecute(Guid id)
        {
            //if (id == Guid.Empty)
            //    throw new ArgumentNullException(nameof(id), "Для удаления товара нужен его Id.");

            //bool operationResult = productRepository.Delete(id);
            //return operationResult;
            throw new NotImplementedException();
        }

        protected override bool SafeDeleteProductExecute(Guid id)
        {
            //if (id == Guid.Empty)
            //    throw new ArgumentNullException(nameof(id), "Для скрытия товара нужен его Id.");

            //bool operationResult = productRepository.SafeDelete(id);
            //return operationResult;
            throw new NotImplementedException();
        }
    }
}
