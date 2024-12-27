using BaseProject.Models;
using DesignPatterns.Command.Models;
using DinkToPdf.Contracts;
using DinkToPdf;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

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

    Enumerable.Range(1, 30).ToList().ForEach(x =>
    {
        identityDbContext.Products.Add(new Product() { Name = $"kalem {x}", Price = x * 100, Stock = x + 50 });
    });

    identityDbContext.SaveChanges();
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
