using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TIA.Core.DTOClasses;

namespace TIA.WebApp.Models
{
    public class YesNoDialogViewModel
    {
        public CatalogObjectDTO ObjectDTO { get; set; }

        public string Controller { get; set; }
    }
}
