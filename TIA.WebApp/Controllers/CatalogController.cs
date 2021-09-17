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
    //[Route("Catalog")]
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

        [Route("GetById/{id:Guid?}")]
        public async Task<ActionResult<CatalogDTO>> GetById(Guid id)
        {
            CatalogDTO catalogDTO = await _tiaModel.GetCatalogByIdAsync(id);

            return View("CatalogProducts", catalogDTO);
        }

        //[Route("GetById/{catalogDTO?}")]
        //public IActionResult GetById(CatalogDTO catalogDTO)
        //{
        //    return View("CatalogProductPage", catalogDTO);
        //}

        //[Route("CreateEdit/{parentId}/{itemId}")]
        public async Task<ActionResult> CreateEdit(Guid? parentId, Guid? itemId)
        {
            CatalogDTO model = new CatalogDTO { IsActive = true };

            List<CatalogDTO> listCatalogs = (await _tiaModel.GetCatalogsLineCollectionAsync())
                .Select(c => new CatalogDTO { Id = c.Id, Title = c.Title }).ToList();

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

            //ViewData["listCatalogs"] = listCatalogs;
            ViewBag.listCatalogs = listCatalogs;

            return PartialView("_CreateEdit", model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> CreateEdit(CatalogDTO model)
        {
            if (!ModelState.IsValid)
                return PartialView("_CreateEdit", model);

            CatalogDTO temp = null;
            if (model.Id == Guid.Empty)
                temp = await _tiaModel.AddCatalogAsync(model);
            else
                temp = await _tiaModel.ChangeCatalogAsync(model);
            

            return RedirectToAction(nameof(this.GetById), temp);
        }


        public IActionResult Delete(CatalogDTO objToRemove)
        {
            try
            {
                ViewData["info_controller"] = "Catalog";
                ViewData["info_action"] = "Delete";
                return PartialView("_YesNoDialog", objToRemove);
            }
            catch(Exception ex)
            {
                return StatusCode(403);
            }
           
        }


        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                bool result = await _tiaModel.DeleteCatalogAsync(new CatalogDTO { Id = id });

                if (result)
                {
                    //return RedirectToAction(nameof(this.CatalogTable));
                    return Ok();
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
