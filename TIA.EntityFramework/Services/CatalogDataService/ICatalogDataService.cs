﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TIA.Core.DTOClasses;

namespace TIA.EntityFramework.Services
{
    public interface ICatalogDataService : IDataService<CatalogDTO>
    {
        Task<IEnumerable<CatalogDTO>> GetCatalogsTree();

        Task<IEnumerable<CatalogDTO>> GetCatalogsLineCollection();
    }
}
