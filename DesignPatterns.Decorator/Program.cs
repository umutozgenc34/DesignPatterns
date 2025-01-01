using BaseProject.Models;
using DesignPatterns.Decorator.Repositories;
using DesignPatterns.Decorator.Repositories.Decorator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
//compile time- scrutor library
builder.Services.AddScoped<IProductRepository, ProductRepository>().Decorate<IProductRepository, ProductRepositoryCacheDecorator>()
    .Decorate<IProductRepository, ProductRepositoryLoggingDecorator>();

//run time
//builder.Services.AddScoped<IProductRepository>(sp =>
//{
//    var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();

//    var context = sp.GetRequiredService<AppIdentityDbContext>();
//    var memoryCache = sp.GetRequiredService<IMemoryCache>();
//    var productRepository = new ProductRepository(context);
//    var logService = sp.GetRequiredService<ILogger<ProductRepositoryLoggingDecorator>>();

//    if (httpContextAccessor.HttpContext.User.Identity.Name == "user1")
//    {
//        var cacheDecorator = new ProductRepositoryCacheDecorator(productRepository, memoryCache);
//        return cacheDecorator;
//    }

//    var logDecorator = new ProductRepositoryLoggingDecorator(productRepository, logService);

//    return logDecorator;
//});


builder.Services.AddDbContext<AppIdentityDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AppIdentityDbContext>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var identityDbContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

    identityDbContext.Database.Migrate();

    if (!userManager.Users.Any())
    {
        await userManager.CreateAsync(new AppUser() { UserName = "umut1", Email = "umut1@gmail.com" }, "Sifre34.");
        await userManager.CreateAsync(new AppUser() { UserName = "ahmet2", Email = "ahmet2@gmail.com" }, "Sifre34.");
        await userManager.CreateAsync(new AppUser() { UserName = "mehmet3", Email = "mehmet3@gmail.com" }, "Sifre34.");
        await userManager.CreateAsync(new AppUser() { UserName = "faruk4", Email = "faruk4@gmail.com.com" }, "Sifre34.");
        await userManager.CreateAsync(new AppUser() { UserName = "melih5", Email = "melih5@gmail.com" }, "Sifre34.");
    }
}

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
