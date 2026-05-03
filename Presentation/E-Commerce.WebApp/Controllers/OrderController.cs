using E_Commerce.Domain.Entities;
using E_CommerceApplication.Dtos.CartDtos;
using E_CommerceApplication.Dtos.CartItemDtos;
using E_CommerceApplication.Dtos.OrderDtos;
using E_CommerceApplication.Dtos.OrderItemDtos;
using E_CommerceApplication.Interfaces;
using E_CommerceApplication.Usecases.CartItemServices;
using E_CommerceApplication.Usecases.CartServices;
using E_CommerceApplication.Usecases.CustomerServices;
using E_CommerceApplication.Usecases.OrderServices;
using E_CommerceApplication.Usecases.ProductServices;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace E_Commerce.WebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderServices _orderServices;
        private readonly ICartService _cartService;
        private readonly IProductServices _productService;
        private readonly ICartItemService _cartItemService;
        private readonly IUserIdentityRepository _userIdentityRepository;
        private readonly ICustomerServices _customerServices;

        public OrderController(IOrderServices orderServices, ICartService cartService, IProductServices productService, ICartItemService cartItemService,IUserIdentityRepository userIdentityRepository, ICustomerServices customerServices)
        {
            _orderServices = orderServices;
            _cartService = cartService;
            _productService = productService;
            _cartItemService = cartItemService;
            _userIdentityRepository = userIdentityRepository;
            _customerServices = customerServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Checkout(int cartId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                string cookieName = "cart";
                CreateCartDto cartItems = new CreateCartDto();

                if (Request.Cookies.TryGetValue(cookieName, out string cartData1))
                {
                    cartItems = JsonSerializer.Deserialize<CreateCartDto>(cartData1) ?? new CreateCartDto();
                }
                GetByIdCartDto detailedCartItems = new GetByIdCartDto();
                detailedCartItems.CartItems = new List<ResultCartItemDto>();
                var totalsum = 0;
                foreach (var item in cartItems.CartItems)
                {

                    // Veritabanından veya servisten `GetByIdCartDto` tipinde detaylı bilgi alın

                    var detailedItem = await _productService.GetByIdProductAsync(item.ProductId);
                    // 🔴 ZORUNLU NULL KONTROLÜ (Checkout için)
                    if (detailedItem == null)
                        continue;

                    var newproduct = new Product();
                    newproduct.ProductId = detailedItem.ProductId;
                    newproduct.ProductName = detailedItem.ProductName;
                    newproduct.Price = detailedItem.Price;
                    newproduct.ImageUrl = detailedItem.ImageUrl;
                    newproduct.Description = detailedItem.Description;
                    newproduct.CategoryId = detailedItem.CategoryId;
                    newproduct.Stock = detailedItem.Stock;

                    totalsum += item.TotalPrice;

                    var newccartitem = new ResultCartItemDto()
                    {
                        Quantity = item.Quantity,
                        ProductId = item.ProductId,
                        TotalPrice = item.TotalPrice,
                        Product = newproduct,
                    };

                    detailedCartItems.CartItems.Add(newccartitem);

                }
                detailedCartItems.TotalAmount = totalsum;
                return View(detailedCartItems);
            }
            else
            {
                var value = await _cartService.GetByIdCartAsync(cartId);
                var userid = await _userIdentityRepository.GetUserIdOnAuth(User);
                var customer = await _customerServices.GetCustomerByUserId(userid);

                value.Customer = new Customer
                {
                    CustomerId = customer.CustomerId,
                    Email = customer.Email,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PhoneNumber = customer.PhoneNumber,
                    UserId = customer.UserId
                };

                if (value == null)
                {
                    return View();
                }
                return View(value);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto dto, int cartId)
        {
            try
            {
                GetByIdCartDto cart;

                if (!User.Identity.IsAuthenticated)
                {
                    string cookieName = "cart";
                    CreateCartDto cartItems = new CreateCartDto();

                    if (Request.Cookies.TryGetValue(cookieName, out string cartData))
                    {
                        cartItems = JsonSerializer.Deserialize<CreateCartDto>(cartData) ?? new CreateCartDto();
                    }

                    GetByIdCartDto detailedCartItems = new GetByIdCartDto
                    {
                        CartItems = new List<ResultCartItemDto>()
                    };

                    int totalSum = 0;

                    foreach (var item in cartItems.CartItems)
                    {
                        var detailedItem = await _productService.GetByIdProductAsync(item.ProductId);

                        // 🔴 NULL KONTROLÜ
                        if (detailedItem == null)
                            continue;

                        var newProduct = new Product
                        {
                            ProductId = detailedItem.ProductId,
                            ProductName = detailedItem.ProductName,
                            Price = detailedItem.Price,
                            ImageUrl = detailedItem.ImageUrl,
                            Description = detailedItem.Description,
                            CategoryId = detailedItem.CategoryId,
                            Stock = detailedItem.Stock
                        };

                        totalSum += item.TotalPrice;

                        detailedCartItems.CartItems.Add(new ResultCartItemDto
                        {
                            Quantity = item.Quantity,
                            ProductId = item.ProductId,
                            TotalPrice = item.TotalPrice,
                            Product = newProduct
                        });
                    }

                    if (!detailedCartItems.CartItems.Any())
                    {
                        return Json(new { success = false, message = "Sepet boş veya ürünler bulunamadı." });
                    }

                    detailedCartItems.TotalAmount = totalSum;
                    detailedCartItems.UserId = "1111111111111";
                    cart = detailedCartItems;
                }
                else
                {
                    cart = await _cartService.GetByIdCartAsync(cartId);
                }

                // 🔴 CART NULL KONTROLÜ
                if (cart?.CartItems == null || !cart.CartItems.Any())
                {
                    return Json(new { success = false, message = "Sepet boş." });
                }

                List<CreateOrderItemDto> orderItems = new List<CreateOrderItemDto>();

                foreach (var item in cart.CartItems)
                {
                    orderItems.Add(new CreateOrderItemDto
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        TotalPrice = item.TotalPrice
                    });
                }

               // dto.CustomerId = 7;
                dto.OrderItems = orderItems;
                dto.OrderStatus = "Siparişiniz Alındı.";

                await _orderServices.CreateOrderAsync(dto);

                // 🔴 Sepeti temizle
                foreach (var item in cart.CartItems)
                {
                    if (item.CartItemId != 0)
                    {
                        await _cartItemService.DeleteCartItemAsync(item.CartItemId);
                    }
                }

                if (cartId != 0)
                {
                    await _cartService.DeleteCartAsync(cartId);
                }

                // 🔴 Cookie temizleme
                string cookieName1 = "cart";
                Response.Cookies.Delete(cookieName1);

                var emptyCart = new CreateCartDto
                {
                    CartItems = new List<CreateCartItemDto>()
                };

                var cartData2 = JsonSerializer.Serialize(emptyCart);
                Response.Cookies.Append(cookieName1, cartData2, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(7),
                    HttpOnly = true,
                    Secure = true
                });

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public async Task<ActionResult> GetCity()
        {
            var values = await _orderServices.GetAllCity();
            return Json(new { success = true, data = values });
        }
        public async Task<ActionResult> GetTownByCityId(int cityId)
        {
            var values = await _orderServices.GetTownByCityId(cityId);
            return Json(new { success = true, data = values });
        }
    }
}
