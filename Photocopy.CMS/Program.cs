using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Photocopy.Core.AppContext;
using Photocopy.Core.Interface.Helper;
using Photocopy.Core.Interface.Repository;
using Photocopy.Core.Interface.Services;
using Photocopy.DataAccess.Repository;
using Photocopy.Helper;
using Photocopy.Service.Services;
using Photocopy.Service.Services.WebUI;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
    
// Add services to the container.
builder.Services.AddControllersWithViews();

#region yzen


builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton(mapper);


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IContentNodeService, ContentNodeService>();
builder.Services.AddScoped<IContentNodeRepository, ContentNodeRepository>();


builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();



builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<ISliderRepository, SliderRepository>();



builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();



builder.Services.AddScoped<IBlogNodeService, BlogNodeService>();
builder.Services.AddScoped<IBlogNodeRepository, BlogNodeRepository>();


builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();


builder.Services.AddScoped<IFaqService, FaqService>();
builder.Services.AddScoped<IFaqRepository, FaqRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ICryptoHelper, CryptoHelper>();

//Some services are not able to be constructed (Error while validating the service descriptor 'ServiceType: Photocopy.Core.Interface.Repository.IUnitOfWork Lifetime: Singleton ImplementationType: Photocopy.DataAccess.Repository.UnitOfWork': Cannot consume scoped service 'Photocopy.Core.AppContext.ApplicationContext' from singleton 'Photocopy.Core.Interface.Repository.IUnitOfWork'.)'
//builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<IUserService, UserService>();



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
     {
         options.LoginPath = "/";
     });
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
#region yzen
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

#endregion

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ContentNode}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=OrderManagement}/{action=Index}/{id?}");
app.Run();
