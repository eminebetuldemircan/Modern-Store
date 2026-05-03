using E_CommerceApplication.Dtos.SubscriberDtos;
using E_CommerceApplication.Usecases.SubscriberServices;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.WebApp.Controllers
{
    public class SubscriberController : Controller
    {
        private readonly ISubscriberService _services;

        public SubscriberController(ISubscriberService services)
        {
            _services = services;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSubscriberDto subscriber)
        {
            subscriber.SubcribeDate = DateTime.Now;
            await _services.CreateSubscriber(subscriber);
            return RedirectToAction("Index", "Home");
        }
    }
}
