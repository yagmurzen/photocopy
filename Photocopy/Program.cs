using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Photocopy.Core.AppContext;
using Photocopy.Core.Interface.Helper;
using Photocopy.Core.Interface.Repository;
using Photocopy.Core.Interface.Services;
using Photocopy.DataAccess.Repository;
using Photocopy.Helper;
using Photocopy.Service.Services;
using Photocopy.Service.Services.WebUI;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpClient();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton(mapper);


builder.Services.AddScoped<IFaqService, FaqService>();
builder.Services.AddScoped<IFaqRepository, FaqRepository>();



builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IContentService, ContentService>();
builder.Services.AddScoped<IContentNodeRepository, ContentNodeRepository>();

builder.Services.AddScoped<IBlogNodeService, BlogNodeService>();
builder.Services.AddScoped<IBlogNodeRepository, BlogNodeRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



builder.Services.AddScoped<ICookieHelper, CookieHelper>();

builder.Services.AddScoped<ICryptoHelper, CryptoHelper>();
builder.Services.AddScoped<IHttpClientExtensions, HttpClientExtensions>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
