using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TIA.BusinessLogicBase.Abstractions;
using TIA.Core.DTOClasses;
using TIA.WebApp.Extentions;
using TIA.WebApp.Models;

namespace TIA.WebApp.Controllers
{
    [Route("Product")]
    public class ProductController : Controller
    {
        private readonly ITiaModel _tiaModel;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ITiaModel tiaModel,
            ILogger<ProductController> logger)
        {
            _tiaModel = tiaModel;
            _logger = logger;
        }

        [Route("GetById/{id:Guid?}")]
        public async Task<ActionResult<ProductDTO>> GetById(Guid id)
        {
            ProductDTO productDTO = await _tiaModel.GetProductByIdAsync(id);

            return null;//View("CatalogProducts", productDTO);
        }

        [HttpGet]
        [Route("CreateEdit/{parentId?}/{itemId?}")]
        public async Task<ActionResult> CreateEdit(Guid? parentId, Guid? itemId)
        {
            ProductDTO model = new ProductDTO { IsActive = true };

            List<CatalogDTO> listCatalogs = (await _tiaModel.GetCatalogsLineCollectionAsync())
                .Select(c => new CatalogDTO { Id = c.Id, Title = c.Title }).ToList();

            ModalProductViewModel vm = new ModalProductViewModel();
            vm.CatalogList = listCatalogs;

            if (itemId != null && itemId != Guid.Empty)
            {
                model = await _tiaModel.GetProductByIdAsync((Guid)itemId);
                vm.ActionType = ActionTypeEnum.Update;
            }
            else
            {
                model = model with { ParentCatalogId = parentId };
                vm.ActionType = ActionTypeEnum.Add;
            }

            vm.IsEmptyCatalog = (await _tiaModel.GetCatalogByIdAsync((Guid)parentId))?.Products.Count() == 0 ? true : false;
            vm.Id = model.Id;
            vm.ParentCatalogId = model.ParentCatalogId;
            vm.IsActive = model.IsActive;
            vm.Title = model.Title;
            vm.Description = model.Description;
            vm.Price = model.Price;

            return PartialView("_CreateEditProduct", vm);
        }

        [HttpPost]
        [Route("CreateEdit/{vm?}")]
        public async Task<ActionResult> CreateEdit(ModalProductViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            ProductDTO product = new ProductDTO
            {
                Id = vm.Id,
                ParentCatalogId = vm.ParentCatalogId,
                IsActive = vm.IsActive,
                Title = vm.Title,
                Description = vm.Description,
                Price = vm.Price
            };
           
            ProductDTO temp = null;
            if (product.Id == Guid.Empty)
                temp = await _tiaModel.AddProductAsync(product);
            else
                temp = await _tiaModel.ChangeProductAsync(product);

            return PartialView("~/Views/Catalog/_ProductTableElementData.cshtml", temp);
        }

        [HttpGet]
        [Route("Delete/{itemId?}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                YesNoDialogViewModel vm = new YesNoDialogViewModel()
                {
                    ObjectDTO = new ProductDTO { Id = id },
                    Controller = "Product"
                };
                return PartialView("_YesNoDialog", vm);
            }
            catch (Exception ex)
            {
                return StatusCode(403);
            }

        }

        [HttpPost]
        [Route("DeleteConfirm/{obj?}")]
        public async Task<ActionResult> DeleteConfirm(ProductDTO obj)
        {
            try
            {
                bool result = await _tiaModel.DeleteProductAsync(obj);

                if (result)
                {
                    return Ok(obj.Id);
                }
                else
                {
                    return StatusCode(409);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
