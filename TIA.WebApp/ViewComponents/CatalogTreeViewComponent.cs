using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TIA.BusinessLogicBase.Abstractions;
using TIA.Core.DTOClasses;

namespace TIA.WebApp.ViewComponents
{
    public class CatalogTreeViewComponent : ViewComponent
    {
        private readonly ITiaModel _tiaModel;

        public CatalogTreeViewComponent(ITiaModel tiaModel)
        {
            _tiaModel = tiaModel;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<CatalogDTO> treeView = await _tiaModel.GetCatalogsTreeAsync();

            return View("DefaultCatalogTree", treeView);
        }
    }
}
