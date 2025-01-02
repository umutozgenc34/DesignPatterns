using BaseProject.Models;
using DesignPatterns.Adapter.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BaseProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IImageProcess _imageProcess;
        public HomeController(ILogger<HomeController> logger,IImageProcess imageProcess)
        {
            _logger = logger;
            _imageProcess = imageProcess;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AddWatermark()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddWatermark(IFormFile image)
        {
            if (image is { Length: >= 0 })
            {
                var imageMemoryStream = new MemoryStream();

                await image.CopyToAsync(imageMemoryStream);

                _imageProcess.AddWatermark("DesignPatterns.Adapter", image.FileName, imageMemoryStream);
            }
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
