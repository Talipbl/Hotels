using Business.Abstracts;
using Business.Concrete;
using DataAccess.Concrete;
using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        IHotelService _hotelService;
        private static List<Hotel> _hotels = new List<Hotel>();
        public HomeController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(),$"wwwroot/files/{file.FileName}");
                using (var stream = new FileStream(path,FileMode.OpenOrCreate))
                {
                    await file.CopyToAsync(stream);
                    stream.Flush();
                }
                var result = _hotelService.Read(path);
                if (result.Success)
                {
                    ListHotelsViewModel model = new ListHotelsViewModel()
                    {
                        Hotels = result.Data,
                        Message = result.Message,
                        Path = path
                    };
                    TempData["message"] = result.Message;
                    _hotels = result.Data;
                    return View(nameof(ShowData),model);
                }
                TempData["message"] = result.Message;
            }
            return View(nameof(Index));
        }
        [HttpGet]
        public IActionResult WriteAsJson(string path)
        {
            var result = _hotelService.WriteAsJson(path,_hotels);
            TempData["message"] = result.Message;
            return View(nameof(Index));
        }
        [HttpGet]
        public IActionResult WriteAsXml(string path)
        {
            var result = _hotelService.WriteAsXml(path, _hotels);
            TempData["message"] = result.Message;
            return View(nameof(Index));
        }
        [HttpGet]
        public IActionResult ShowData(ListHotelsViewModel model)
        {
            return View(model);
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