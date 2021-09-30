using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TIA.BusinessLogicLayerBase.Abstractions;
using TIA.DataAccessLayer.DTOClasses;
using TIA.RestAPI.Models;

namespace TIA.RestAPI.Controllers
{

    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("api/Catalogs")]
    public class CatalogsController : Controller
    {
        private readonly ITiaModel _tiaModel;
        private readonly ILogger<CatalogsController> _logger;

        public CatalogsController(ITiaModel tiaModel,
            ILogger<CatalogsController> logger)
        {
            _tiaModel = tiaModel;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetCatalogs")]
        public async Task<ActionResult<JsonCoreObject<List<CatalogDTO>>>> GetCatalogs()
        {
            try
            {
                List<CatalogDTO> catalogs = await _tiaModel.GetCatalogsLineCollectionAsync();

                return new JsonCoreObject<List<CatalogDTO>> { Object = catalogs };
            }
            catch (Exception)
            {
                return new JsonCoreObject<List<CatalogDTO>> { Object = null };
            }
        }

        [HttpGet]
        [Authorize]
        [Route("Products/{id:Guid?}/{minDate?}/{maxDate?}/{minPrice?}/{maxPrice?}")]
        public async Task<ActionResult<JsonCoreObject<CatalogDTO>>> Products(Guid? id, string title, DateTime? minDate, DateTime? maxDate, uint? minPrice, uint? maxPrice)
        {
            try
            {
                if (id != null && id != Guid.Empty)
                {
                    Guid nId = (Guid)id;

                    CatalogDTO catalogDTO = await _tiaModel.GetCatalogByIdAsync(nId);

                    if (catalogDTO != null)
                    {
                        List<ProductDTO> products = await _tiaModel.GetCatalogProductsWithFiltersAsync(nId, title, minDate, maxDate, minPrice, maxPrice);

                        catalogDTO = catalogDTO with { Products = products };

                        return new JsonCoreObject<CatalogDTO> { Object = catalogDTO };
                    }
                }
                return new JsonCoreObject<CatalogDTO> { Object = null };
            }
            catch (Exception)
            {
                return new JsonCoreObject<CatalogDTO> { Object = null };
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("Create/{model?}")]
        public async Task<ActionResult<JsonCoreObject<CatalogDTO>>> Create(InputCatalogData model)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                }

                CatalogDTO dto = new CatalogDTO
                {
                    Id = model.Id,
                    Title = model.Title,
                    Description = model.Description,
                    ParentCatalogId = model.ParentCatalogId,
                    IsActive = model.IsActive
                };

                if (dto.ParentCatalogId == Guid.Empty)
                    dto = dto with { ParentCatalogId = null };

                CatalogDTO temp = await _tiaModel.AddCatalogAsync(dto);


                return new JsonCoreObject<CatalogDTO> { Object = temp };
            }
            catch (Exception)
            {
                return new JsonCoreObject<CatalogDTO> { Object = null };
            }

        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("Update/{model?}")]
        public async Task<ActionResult<JsonCoreObject<CatalogDTO>>> Update(InputCatalogData model)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                }

                CatalogDTO dto = new CatalogDTO
                {
                    Id = model.Id,
                    Title = model.Title,
                    Description = model.Description,
                    ParentCatalogId = model.ParentCatalogId,
                    IsActive = model.IsActive
                };

                if (dto.ParentCatalogId == Guid.Empty)
                    dto = dto with { ParentCatalogId = null };

                CatalogDTO temp = await _tiaModel.ChangeCatalogAsync(dto);

                return new JsonCoreObject<CatalogDTO> { Object = temp };
            }
            catch (Exception)
            {
                return new JsonCoreObject<CatalogDTO> { Object = null };
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        [Route("Delete/{id?}")]
        public async Task<ActionResult<JsonCoreObject<bool>>> Delete(Guid id)
        {
            try
            {
                bool result = await _tiaModel.DeleteCatalogAsync(id);

                if (result)
                {
                    return new JsonCoreObject<bool> { Object = true };
                }
                else
                {
                    return new JsonCoreObject<bool> { Object = false };
                }
            }
            catch (Exception)
            {
                return new JsonCoreObject<bool> { Object = false };
            }
        }
    }
}
