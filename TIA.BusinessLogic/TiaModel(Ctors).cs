using TIA.BusinessLogicBase;
using TIA.EntityFramework.Services;

namespace TIA.BusinessLogic
{
    public partial class TiaModel : TiaModelBase
    {
        protected readonly ICatalogDataService catalogDataService;
        protected readonly IProductDataService productDataService;

        public TiaModel(ICatalogDataService catalogDataService,
            IProductDataService productDataService)
        {
            this.catalogDataService = catalogDataService;
            this.productDataService = productDataService;
        }
    }
}
