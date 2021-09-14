using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TIA.BusinessLogicBase.Abstractions;
using TIA.WebApp.Models;

namespace TIA.WebApp.Controllers
{
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

        public async Task<IActionResult> Index()
        {
            CatalogViewModel cVm = new CatalogViewModel
            {
                Catalog = null,
                CatalogTree = await _tiaModel.GetCatalogsTreeAsync()
            };
          
            return View(cVm);
        }

        public async Task<IActionResult> GetById(Guid id)
        {
            CatalogViewModel cVm = new CatalogViewModel
            {
                Catalog = await _tiaModel.GetCatalogByIdAsync(id),
                CatalogTree = await _tiaModel.GetCatalogsTreeAsync()
            };

            return View(cVm);
        }
    }
}
