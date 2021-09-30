using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TIA.BusinessLogicLayerBase.Abstractions;
using TIA.DataAccessLayer.DTOClasses;
using TIA.DataAccessLayer.Models;
using TIA.RestAPI.Models;

namespace TIA.RestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly ITiaModel _tiaModel;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ITiaModel tiaModel,
            ILogger<ProductsController> logger)
        {
            _tiaModel = tiaModel;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetProductsWithFilter/{minDate?}/{maxDate?}/{minPrice?}/{maxPrice?}")]
        public async Task<ActionResult<JsonCoreObject<List<ProductDataModel>>>> GetProductsWithFilter(DateTime? minDate, DateTime? maxDate, uint? minPrice, uint? maxPrice)
        {
            try
            {
                List<ProductDataModel> products = await _tiaModel.GetProductsFullDataAsync(minDate, maxDate, minPrice, maxPrice);

                return new JsonCoreObject<List<ProductDataModel>> { Object = products };
            }
            catch (Exception)
            {
                return new JsonCoreObject<List<ProductDataModel>> { Object = null };
            }
        }

        [HttpGet]
        [Route("GetProduct/{id?}")]
        public async Task<ActionResult<JsonCoreObject<ProductDTO>>> GetProduct(Guid id)
        {
            try
            {
                ProductDTO dto = await _tiaModel.GetProductByIdAsync(id);

                return new JsonCoreObject<ProductDTO> { Object = dto };
            }
            catch (Exception)
            {
                return new JsonCoreObject<ProductDTO> { Object = null };
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("Create/{model?}")]
        public async Task<ActionResult<JsonCoreObject<ProductDTO>>> Create(InputProductData model)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                }

                ProductDTO product = new ProductDTO
                {
                    Id = model.Id,
                    ParentCatalogId = model.ParentCatalogId,
                    IsActive = model.IsActive,
                    Title = model.Title,
                    Description = model.Description,
                    Price = model.Price
                };

                ProductDTO temp = await _tiaModel.AddProductAsync(product);

                return new JsonCoreObject<ProductDTO> { Object = temp };
            }
            catch (Exception)
            {
                return new JsonCoreObject<ProductDTO> { Object = null };
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("Update/{model?}")]
        public async Task<ActionResult<JsonCoreObject<ProductDTO>>> Update(InputProductData model)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                }
                    
                ProductDTO product = new ProductDTO
                {
                    Id = model.Id,
                    ParentCatalogId = model.ParentCatalogId,
                    IsActive = model.IsActive,
                    Title = model.Title,
                    Description = model.Description,
                    Price = model.Price
                };

                ProductDTO temp = await _tiaModel.ChangeProductAsync(product);
                   
                return new JsonCoreObject<ProductDTO> { Object = temp };
            }
            catch (Exception)
            {
                return new JsonCoreObject<ProductDTO> { Object = null };
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        [Route("Delete/{id?}")]
        public async Task<ActionResult<JsonCoreObject<bool>>> Delete(Guid id)
        {
            try
            {
                bool result = await _tiaModel.DeleteProductAsync(id);

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
