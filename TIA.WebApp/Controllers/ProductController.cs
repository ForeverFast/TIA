using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TIA.BusinessLogicBase.Abstractions;

namespace TIA.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ITiaModel _tiaModel;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ITiaModel tiaModel,
            ILogger<ProductController> logger)
        {
            _tiaModel = tiaModel;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
