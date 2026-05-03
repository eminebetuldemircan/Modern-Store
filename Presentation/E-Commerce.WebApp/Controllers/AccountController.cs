using E_CommerceApplication.Dtos.AccountDtos;
using E_CommerceApplication.Dtos.CustomerDtos;
using E_CommerceApplication.Dtos.OrderDtos;
using E_CommerceApplication.Interfaces;
using E_CommerceApplication.Usecases.AccountService;
using E_CommerceApplication.Usecases.CustomerServices;
using E_CommerceApplication.Usecases.OrderItemServices;
using E_CommerceApplication.Usecases.OrderServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ICustomerServices _customerServices;
        private readonly IUserIdentityRepository _userIdentityRepository;
        private readonly IOrderServices _orderServices;
        private readonly IOrderItemServices _orderItemService;

        public AccountController(IAccountService accountService, ICustomerServices customerServices, IUserIdentityRepository userIdentityRepository, IOrderServices orderServices, IOrderItemServices orderItemService)
        {
            _accountService = accountService;
            _customerServices = customerServices;
            _userIdentityRepository = userIdentityRepository;
            _orderServices = orderServices;
            _orderItemService = orderItemService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var values = await _accountService.Login(dto);

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var values = await _accountService.Register(dto);

            var customer = new CreateCustomerDto
            {
                Email = dto.Email,
                FirstName = dto.Name,
                LastName = dto.Surname,
                UserId = values,
                PhoneNumber = dto.PhoneNumber,
            };
            await _customerServices.CreateCustomerAsync(customer);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var userid = await _userIdentityRepository.GetUserIdOnAuth(User);
            var user = await _customerServices.GetCustomerByUserId(userid);

            if (user == null)
            {
                TempData["Error"] = "Profil bilgileri bulunamadı. Lütfen destek ile iletişime geçin.";
                return RedirectToAction("Index", "Home");
            }

            var order = await _orderServices.GetOrderByUserId(userid);
            var result = new ResultProfileDto
            {
                UserId = userid,
                Name = user.FirstName,
                Surname = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Orders = order,
            };
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(string name, string surname)
        {
            var userId = await _accountService.GetUserIdAsync(User);
            var result = await _accountService.UpdateUser(userId, name, surname);
            await _customerServices.UpdateNameAndSurname(userId, name, surname);
            return RedirectToAction("Profile", "Account");

        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
        {
            var userid = await _accountService.GetUserIdAsync(User);
            model.UserId = userid;
            var result = await _accountService.ChangePassword(model);
            return RedirectToAction("Profile", "Account");
        }
    }
}
