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

            return View("CatalogProducts", productDTO);
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

            vm.ProductDTO = model;

            return PartialView("_CreateEditProduct", vm);
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        [Route("CreateEdit/{product?}")]
        public async Task<ActionResult> CreateEdit(ProductDTO product)
        {
            if (!ModelState.IsValid)
                return PartialView("_CreateEditProduct", product);

            ProductDTO temp = null;
            if (product.Id == Guid.Empty)
                temp = await _tiaModel.AddProductAsync(product);
            else
                temp = await _tiaModel.ChangeProductAsync(product);
            
            return PartialView("_ProductTableData", temp);
        }
    }
}
