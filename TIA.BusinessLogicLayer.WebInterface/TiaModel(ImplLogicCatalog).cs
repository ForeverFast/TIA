using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using TIA.BusinessLogicLayerBase;
using TIA.DataAccessLayer.DTOClasses;

namespace TIA.BusinessLogicLayer.WebInterface
{
    public partial class TiaModel : TiaModelBase
    {
        protected override CatalogDTO GetCatalogByIdExecute(Guid id)
        {
            var client = new RestClient($"http://localhost:18062/api/Catalogs/GetCatalog?id={id}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", Token);
            IRestResponse response = client.Execute(request);



            return null;
        }

        protected override List<ProductDTO> GetCatalogProductsWithFiltersExecute(Guid id, string title, DateTime? minDate, DateTime? maxDate, uint? minPrice, uint? maxPrice)
        {
            throw new NotImplementedException();
        }

        protected override List<CatalogDTO> GetCatalogsTreeExecute()
        {
            var client = new RestClient("http://localhost:18062/api/Catalogs/GetCatalogs");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", Token);

            IRestResponse response = client.Execute(request);

            var data = JsonConvert.DeserializeObject<JsonCoreObject<List<CatalogDTO>>>(response.Content);

            return data?.Object.Where(c => c.ParentCatalogId == null).ToList();

        }

        protected override List<CatalogDTO> GetCatalogsLineCollectionExecute()
        {
            //List<CatalogDTO> tree = catalogRepository.GetCatalogsLineCollection();
            //return tree;
            throw new NotImplementedException();
        }

        protected override CatalogDTO AddCatalogExecute(CatalogDTO catalogDTO)
        {
            //if (string.IsNullOrEmpty(catalogDTO.Title))
            //    throw new ArgumentNullException(nameof(catalogDTO.Title), "У каталога должно быть название.");

            //CatalogDTO dbCreatedCatalog = catalogRepository.Add(catalogDTO);
            //return dbCreatedCatalog;
            throw new NotImplementedException();
        }

        protected override CatalogDTO ChangeCatalogExecute(CatalogDTO catalogDTO)
        {
            //if (catalogDTO.Id == Guid.Empty)
            //    throw new ArgumentNullException(nameof(catalogDTO.Id), "Для изменение каталога нужен его Id.");

            //if (string.IsNullOrEmpty(catalogDTO.Title))
            //    throw new ArgumentNullException(nameof(catalogDTO.Title), "У каталога должно быть название.");

            //CatalogDTO dbChangedCatalog = catalogRepository.Update(catalogDTO, catalogDTO.Id);
            //return dbChangedCatalog;
            throw new NotImplementedException();
        }


        protected override bool DeleteCatalogExecute(Guid id)
        {
            //if (id == Guid.Empty)
            //    throw new ArgumentNullException(nameof(id), "Для скрытия каталога нужен его Id.");

            //CatalogDTO catalog = catalogRepository.GetById(id);
            //if (catalog.Products.Count > 0 || catalog.Catalogs.Count > 0)
            //    return false;

            //bool operationResult =  catalogRepository.Delete(id);
            //return operationResult;
            throw new NotImplementedException();
        }
    }
}
