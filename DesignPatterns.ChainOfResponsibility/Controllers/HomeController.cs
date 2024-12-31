using BaseProject.Models;
using DesignPatterns.ChainOfResponsibility.ChainOfResponsibility;
using DesignPatterns.ChainOfResponsibility.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BaseProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppIdentityDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, AppIdentityDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateZip()
        {
            var products = await _context.Products.ToListAsync();

            var excelProcessHandler = new ExcelProcessHandler<Product>();
            var zipFileProcessHandler = new ZipFileProcessHandler<Product>(_httpContextAccessor);

            excelProcessHandler.SetNext(zipFileProcessHandler);

            await excelProcessHandler.handle(products);

            return View(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
