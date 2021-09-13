using System;
using System.Threading.Tasks;
using TIA.Core.DTOClasses;

namespace TIA.EntityFramework.Services
{
    public interface IProductDataService : IDataService<ProductDTO>
    {
        Task<bool> SafeDelete(Guid guid);
    }
}
