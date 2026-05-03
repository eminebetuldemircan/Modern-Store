using E_CommerceApplication.Dtos.ContactDtos;
using E_CommerceApplication.Usecases.ContactServices;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.WebApp.Controllers
{

    public class ContactController : Controller
    {
        private readonly IContactService _services;
        public ContactController(IContactService services)
        {
            _services = services;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto dto)
        {
            dto.CreatedDate = DateTime.Now;
            await _services.CreateContactAsync(dto);
            return RedirectToAction("Index");
        }
    }
}
