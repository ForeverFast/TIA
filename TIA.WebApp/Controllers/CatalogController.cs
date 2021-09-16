using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
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

        ////[Route("Index")]
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [Route("GetById/{id:Guid?}")]
        public async Task<ActionResult<CatalogDTO>> GetById(Guid id)
        {
            CatalogDTO catalogDTO = await _tiaModel.GetCatalogByIdAsync(id);

            return View("CatalogProductPage", catalogDTO);
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
          
            if (itemId != null && itemId != Guid.Empty)
            {
                model = await _tiaModel.GetCatalogByIdAsync((Guid)itemId);
            }
            else
            {
                model = model with { ParentCatalogId = parentId };
            }

            return PartialView("_CreateEdit", model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        //[Route("CreateEdit/{model}")]
        public async Task<ActionResult> CreateEdit(CatalogDTO model)
        {
            //validate user  
            if (!ModelState.IsValid)
                return PartialView("_CreateEdit", model);

            CatalogDTO dbCreatedCatalog = await _tiaModel.AddCatalogAsync(model);

            //save user into database   
            return RedirectToAction("GetById", dbCreatedCatalog);
        }


    }
}
