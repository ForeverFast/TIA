using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TIA.BusinessLogicLayer.WebInterface;
using TIA.BusinessLogicLayerBase.Abstractions;
using TIA.DataAccessLayer.DTOClasses;
using TIA.DataAccessLayer.Models;
using TIA.WebInterface.Models;

namespace TIA.WebInterface.Controllers
{
    [Route("Product")]
    public class ProductController : Controller
    {
        private readonly ITiaModelWebInterface _tiaModel;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ITiaModelWebInterface tiaModel,
            ILogger<ProductController> logger)
        {
            _tiaModel = tiaModel;
            _logger = logger;


        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _tiaModel.Token = HttpContext.User?.Claims
              .FirstOrDefault(c => c.Type == "token")?.Value;
            base.OnActionExecuting(context);
        }

        //[Route("ProductDetailPage/{id:Guid?}")]
        //public async Task<ActionResult<ProductDTO>> ProductDetailPage(Guid id)
        //{
        //    ProductDTO productDTO = await _tiaModel.GetProductByIdAsync(id);

        //    return null;//View("CatalogProducts", productDTO);
        //}

        [Route("ProductFullData")]
        public IActionResult ProductFullData()
        {
            return View();
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
    }
}
