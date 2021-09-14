using TIA.BusinessLogicBase.Abstractions;
using TIA.EntityFramework.Services;

namespace TIA.BusinessLogicBase
{
    public abstract partial class TiaModelBase : ITiaModel
    {
        protected readonly ICatalogDataService catalogDataService;
        protected readonly IProductDataService productDataService;
    }
}
