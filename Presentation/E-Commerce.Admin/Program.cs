using E_Commerce.Persistence.Context;
using E_Commerce.Persistence.Context.Idendity;
using E_Commerce.Persistence.Context.Identity;
using E_Commerce.Persistence.Repositories;
using E_Commerce.Persistence.Repositories.CartItemsRepository;
using E_Commerce.Persistence.Repositories.CartsRepository;
using E_Commerce.Persistence.Repositories.FavoritesRepository;
using E_Commerce.Persistence.Repositories.OrdersRepository;
using E_Commerce.Persistence.Repositories.ProductsRepository;
using E_CommerceApplication.Interfaces;
using E_CommerceApplication.Interfaces.ICartItemsRepository;
using E_CommerceApplication.Interfaces.ICartsRepository;
using E_CommerceApplication.Interfaces.IFavoritesRepository;
using E_CommerceApplication.Interfaces.IOrderRepository;
using E_CommerceApplication.Interfaces.IProductRepository;
using E_CommerceApplication.Usecases.AccountService;
using E_CommerceApplication.Usecases.CartItemServices;
using E_CommerceApplication.Usecases.CartServices;
using E_CommerceApplication.Usecases.CategoryServices;
using E_CommerceApplication.Usecases.ContactServices;
using E_CommerceApplication.Usecases.CustomerServices;
using E_CommerceApplication.Usecases.EmailServices;
using E_CommerceApplication.Usecases.EMailServices;
using E_CommerceApplication.Usecases.FavoritesServices;
using E_CommerceApplication.Usecases.HelpServices;
using E_CommerceApplication.Usecases.OrderItemServices;
using E_CommerceApplication.Usecases.OrderServices;
using E_CommerceApplication.Usecases.ProductServices;
using E_CommerceApplication.Usecases.SubscriberServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shoper.Application.Usecasess.CategoryServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserIdentityRepository, UserIdentityRepository>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<IOrderRepository, OrdersRepository>();
builder.Services.AddScoped<ICartsRepository, CartsRepository>();
builder.Services.AddScoped<ICartItemsRepository, CartItemsRepository>();
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartItemService, CartItemService>();
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<IOrderItemServices, OrderItemServices>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ISubscriberService, SubscriberService>();
builder.Services.AddScoped<E_CommerceApplication.Usecases.HelpServices.IHelpService, HelpService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<ICustomerServices, CustomerServices>();
builder.Services.AddScoped<IFavoritesService, FavoritesService>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IFavoritesRepository, FavoritesRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
{
    var configuration = builder.Configuration;
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Giriþ yapmadan eriþilmeye çalýþýldýðýnda yönlendirilecek sayfa
    });
builder.Services.AddIdentity<AppIdentityUser, AppIdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{

    //options.Password.RequireDigit = true; //Þifre Sayýsal karakteri desteklesin mi?
    options.Password.RequiredLength = 6;  //Þifre minumum karakter sayýsý
    options.Password.RequireLowercase = true; //Þifre küçük harf olabilir
    options.Password.RequireLowercase = true; //Þifre büyük harf olabilir
    options.Password.RequireNonAlphanumeric = false; //Sembol bulunabilir

    options.Lockout.MaxFailedAccessAttempts = 5; //Kullanýcý kaç baþarýsýz giriþten sonra sisteme giriþ yapamasýn
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); //Baþarýsýz giriþ iþlemlerinden sonra ne kadar süre sonra sisteme giriþ hakký tanýnsýn
    options.Lockout.AllowedForNewUsers = true; //Yeni üyeler için kilit sistemi geçerli olsun mu

    options.User.RequireUniqueEmail = true; //Kullanýcý benzersiz e-mail adresine sahip olsun

    options.SignIn.RequireConfirmedEmail = false; //Kayýt iþlemleri için email onaylamasý zorunlu olsun mu?
    options.SignIn.RequireConfirmedPhoneNumber = false; //Telefon onayý olsun mu?
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();