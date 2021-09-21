using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
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
                    return View("CatalogProducts", catalogDTO);
            }
            return RedirectToPage("~/View/Shared/NotFoundPage");
        }

        [HttpGet]
        [Route("GetEmptyTable")]
        public IActionResult GetEmptyTable()
        {
            return PartialView("_TableView");
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

        [HttpPost]
        [Route("CreateEdit")]
        public async Task<ActionResult> CreateEdit(ModalCatalogViewModel vm)
        {
            if (!ModelState.IsValid)
                return PartialView("_CreateEditCatalog", vm);

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


        [HttpGet]
        [Route("Delete/{itemId?}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                YesNoDialogViewModel vm = new YesNoDialogViewModel()
                {
                    ObjectDTO = new CatalogDTO { Id = id },
                    Controller = "Catalog"
                };
                return PartialView("_YesNoDialog", vm);
            }
            catch(Exception ex)
            {
                return StatusCode(403);
            }
           
        }

        [HttpPost]
        [Route("DeleteConfirm/{obj?}")]
        public async Task<ActionResult> DeleteConfirm(CatalogDTO obj)
        {
            try
            {
                bool result = await _tiaModel.DeleteCatalogAsync(obj);

                if (result)
                {
                    return Ok(obj.Id);
                }
                else
                {
                    return StatusCode(409);
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
