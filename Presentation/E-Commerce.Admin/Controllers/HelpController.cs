using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using E_CommerceApplication.Usecases.HelpServices;

namespace E_Commerce.Admin.Controllers
{
    public class HelpController : Controller
    {
        private readonly E_CommerceApplication.Usecases.HelpServices.IHelpService _helpService;

        public HelpController(E_CommerceApplication.Usecases.HelpServices.IHelpService helpService)
        {
            _helpService = helpService;
        }

        public async Task<IActionResult> Index()
        {
            var helps = await _helpService.GetAllHelpAsync();
            return View(helps);
        }
        public async Task<IActionResult> Detail(int helpId)
        {
            var help = await _helpService.GetByIdHelpAsync(helpId);
            return View(help);
        }
        public async Task<IActionResult> Delete(int helpId)
        {
            await _helpService.DeleteHelpAsync(helpId);
            return RedirectToAction("Index");
        }
    }
}
