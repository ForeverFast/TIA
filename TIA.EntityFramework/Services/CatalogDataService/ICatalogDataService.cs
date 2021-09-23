using System.Collections.Generic;
using System.Threading.Tasks;
using TIA.Core.DTOClasses;

namespace TIA.EntityFramework.Services
{
    public interface ICatalogDataService : IDataService<CatalogDTO>
    {
        Task<List<CatalogDTO>> GetCatalogsTree();

        Task<List<CatalogDTO>> GetCatalogsLineCollection();
    }
}
