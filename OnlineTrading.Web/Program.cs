using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Connections;
using OnlineTrading.Repository;
using OnlineTrading.Repository.Implementations;
using OnlineTrading.Repository.Interfaces.BankAccount;
using OnlineTrading.Repository.Interfaces.DeliveryService;
using OnlineTrading.Repository.Interfaces.Offer;
using OnlineTrading.Repository.Interfaces.Order;
using OnlineTrading.Repository.Interfaces.Product;
using OnlineTrading.Repository.Interfaces.ProductOrder;
using OnlineTrading.Repository.Interfaces.Shop;
using OnlineTrading.Repository.Interfaces.User;
using OnlineTrading.Service.Implementations.OnlineTrading.Service.Implementations;
using OnlineTrading.Service.Implementations;
using OnlineTrading.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IShopRepository, ShopRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOfferRepository, OfferRepository>();
builder.Services.AddScoped<IDeliveryServiceRepository, DeliveryServiceRepository>();
builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();
builder.Services.AddScoped<IProductOrderRepository, ProductOrderRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IShopService, ShopService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddScoped<IDeliveryServiceService, DeliveryServiceService>();
builder.Services.AddScoped<IBankAccountService, BankAccountService>();
builder.Services.AddScoped<IProductOrderService, ProductOrderService>();

builder.Services.AddScoped<OnlineTrading.Service.Intterfaces.IAuthenticationService, OnlineTrading.Service.Implementations.AuthenticationService>();

builder.Services.AddSession();

ConnectionFactory.Initialize(builder.Configuration.GetConnectionString("DefaultConnection"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();