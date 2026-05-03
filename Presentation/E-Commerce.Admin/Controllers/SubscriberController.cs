using E_CommerceApplication.Usecases.SubscriberServices;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Admin.Controllers
{

    public class SubscriberController : Controller
    {
        private readonly ISubscriberService _subscriberService;

        public SubscriberController(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }

        public async Task<IActionResult> Index()
        {
            var value = await _subscriberService.GetAllSubscribers();
            return View(value);
        }
    }
}
