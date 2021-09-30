using TIA.BusinessLogicLayerBase;
using TIA.DataAccessLayer.Repositories;

namespace TIA.BusinessLogicLayer
{
    public partial class TiaModel : TiaModelBase
    {
        protected readonly ICatalogRepository catalogRepository;
        protected readonly IProductRepository productRepository;

        public TiaModel(ICatalogRepository catalogRepository,
            IProductRepository productRepository)
        {
            this.catalogRepository = catalogRepository;
            this.productRepository = productRepository;
        }
    }
}
