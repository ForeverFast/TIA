using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TIA.BusinessLogicBase.Abstractions;
using TIA.Core.DTOClasses;
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

        [Route("ProductDetailPage/{id:Guid?}")]
        public async Task<ActionResult<ProductDTO>> ProductDetailPage(Guid id)
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
            }
            else
            {
                model = model with { ParentCatalogId = parentId };
            }
            CatalogDTO prCtlg = await _tiaModel.GetCatalogByIdAsync((Guid)parentId);
            vm.IsEmptyCatalog = prCtlg?.Products.Count() == 0 ? true : false;
            vm.Id = model.Id;
            vm.ParentCatalogId = model.ParentCatalogId;
            vm.IsActive = model.IsActive;
            vm.Title = model.Title;
            vm.Description = model.Description;
            vm.Price = model.Price;

            return PartialView("_CreateEditProduct", vm);
        }

        [ValidateAntiForgeryToken]
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

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("Delete/{id?}")]
        public async Task<ActionResult> DeleteConfirm(Guid id)
        {
            bool result = await _tiaModel.DeleteProductAsync(id);

            if (result)
            {
                return Ok(new List<Guid> { id });
            }
            else
            {
                return StatusCode(409);
            }
        }
    }
}
