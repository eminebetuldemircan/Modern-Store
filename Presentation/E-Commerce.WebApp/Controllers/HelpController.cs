using E_CommerceApplication.Dtos.HelpDtos;
using E_CommerceApplication.Usecases.HelpServices;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace E_Commerce.WebApp.Controllers
{
    public class HelpController : Controller
    {
        private readonly IHelpService _services;
        public HelpController(IHelpService services)
        {
            _services = services;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateHelp(CreateHelpDto dto)
        {
            dto.CreatedDate = DateTime.Now;
            dto.Status = 0;
            await _services.CreateHelpAsync(dto);
            return RedirectToAction("Index");
        }
    }
}
