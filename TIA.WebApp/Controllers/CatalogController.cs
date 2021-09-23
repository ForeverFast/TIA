using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TIA.BusinessLogicBase.Abstractions;
using TIA.Core.DTOClasses;
using TIA.Extentions;
using TIA.WebApp.Models;

namespace TIA.WebApp.Controllers
{
    [Route("Catalog")]
    public class CatalogController : Controller
    {
        private readonly ITiaModel _tiaModel;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(ITiaModel tiaModel,
            ILogger<CatalogController> logger)
        {
            _tiaModel = tiaModel;
            _logger = logger;
        }

        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("CatalogTable")]
        public async Task<IActionResult> CatalogTable()
        {
            List<CatalogDTO> catalogDTOs = await _tiaModel.GetCatalogsLineCollectionAsync();

            return View(catalogDTOs);
        }

        [Route("Products/{id:Guid?}")]
        public async Task<ActionResult<CatalogDTO>> Products(Guid? id)
        {
            if (id != null && id != Guid.Empty)
            {
                CatalogDTO catalogDTO = await _tiaModel.GetCatalogByIdAsync((Guid)id);

                if (catalogDTO != null)
                {
                    return View("CatalogProducts", catalogDTO);
                }
                    
            }
            return RedirectToPage("~/View/Shared/NotFoundPage");
        }

        [Route("TableData/{id:Guid?}")]
        public async Task<ActionResult<CatalogDTO>> TableData(Guid? id)
        {
            if (id != null && id != Guid.Empty)
            {
                CatalogDTO catalogDTO = await _tiaModel.GetCatalogByIdAsync((Guid)id);

                if (catalogDTO != null)
                {
                    return PartialView("_TableView", catalogDTO);
                }

            }
            return RedirectToPage("~/View/Shared/NotFoundPage");
        }

        [HttpGet]
        [Route("CreateEdit/{parentId?}/{itemId?}")]
        public async Task<ActionResult> CreateEdit(Guid? parentId, Guid? itemId)
        {
            CatalogDTO model = new CatalogDTO { IsActive = true };

            List<CatalogDTO> listCatalogs = (await _tiaModel.GetCatalogsLineCollectionAsync())
                .Select(c => new CatalogDTO { Id = c.Id, Title = c.Title }).ToList();

            listCatalogs.Add(new CatalogDTO { Title = "Корневой каталог" });

            if (itemId != null && itemId != Guid.Empty)
            {
                model = await _tiaModel.GetCatalogByIdAsync((Guid)itemId);
                CatalogDTO modelElement = listCatalogs.FirstOrDefault(c => c.Id == model.Id);
                IEnumerable<CatalogDTO> chCatalogs = DataExtentions.SelectRecursive(model.Catalogs, c => c.Catalogs);
                foreach (CatalogDTO chCatalog in chCatalogs)
                    listCatalogs.Remove(listCatalogs.FirstOrDefault(c => c.Id == chCatalog.Id));
                listCatalogs.Remove(modelElement);
            }
            else
            {
                model = model with { ParentCatalogId = parentId };
            }

            ModalCatalogViewModel vm = new ModalCatalogViewModel();
            vm.CatalogList = listCatalogs;
            vm.CatalogDTO = model;

            return PartialView("_CreateEditCatalog", vm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("CreateEdit/{vm?}")]
        public async Task<ActionResult> CreateEdit(ModalCatalogViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            CatalogDTO model = vm.CatalogDTO;

            if (model.ParentCatalogId == Guid.Empty)
                model = model with { ParentCatalogId = null };

            CatalogDTO temp = null;
            if (model.Id == Guid.Empty)
                temp = await _tiaModel.AddCatalogAsync(model);
            else
                temp = await _tiaModel.ChangeCatalogAsync(model);


            return RedirectToAction(nameof(this.Products), temp);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("Delete/{id?}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            bool result = await _tiaModel.DeleteCatalogAsync(id);

            if (result)
            {
                return Ok(id);
            }
            else
            {
                return StatusCode(409);
            }
        }
    }
}
