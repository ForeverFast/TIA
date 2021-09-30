using TIA.BusinessLogicLayerBase.Abstractions;

namespace TIA.BusinessLogicLayerBase
{
    public abstract partial class TiaModelBase : ITiaModel, ICatalogModel, IProductModel
    {
        protected TiaModelBase()
        {
           
        }
    }
}
