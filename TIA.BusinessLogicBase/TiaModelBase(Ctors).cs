using TIA.BusinessLogicBase.Abstractions;
using TIA.EntityFramework.Services;

namespace TIA.BusinessLogicBase
{
    public abstract partial class TiaModelBase : ITiaModel, ICatalogModel, IProductModel
    {
        protected TiaModelBase(ICatalogDataService catalogDataService,
            IProductDataService productDataService)
        {
            this.catalogDataService = catalogDataService;
            this.productDataService = productDataService;
        }
    }
}
