global using BlazorEcommerce.Shared;
global using BlazorEcommerce.Client.Services.ProductService;
global using BlazorEcommerce.Client.Services.CategoryService;
global using BlazorEcommerce.Client.Services.AuthService;
global using Microsoft.AspNetCore.Components.Authorization;
global using BlazorEcommerce.Client.Services.CartService;
global using BlazorEcommerce.Client.Services.OrderService;
global using BlazorEcommerce.Client.Services.AddressService;
global using BlazorEcommerce.Client.Services.ProductTypeService;
using BlazorEcommerce.Client;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;



var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IProductTypeService, ProductTypeService>();

await builder.Build().RunAsync();
