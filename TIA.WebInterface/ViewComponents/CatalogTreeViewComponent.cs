using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TIA.BusinessLogicLayer.WebInterface;
using TIA.DataAccessLayer.DTOClasses;

namespace TIA.WebApp.ViewComponents
{
    public class CatalogTreeViewComponent : ViewComponent
    {
        private readonly ITiaModelWebInterface _tiaModel;

        public CatalogTreeViewComponent(ITiaModelWebInterface tiaModel)
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
