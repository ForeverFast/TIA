using TIA.BusinessLogicBase;
using TIA.EntityFramework.Services;

namespace TIA.BusinessLogic
{
    public partial class TiaModel : TiaModelBase
    {
        public TiaModel(ICatalogDataService catalogDataService,
            IProductDataService productDataService) : base(catalogDataService, productDataService)
        {

        }
    }
}
