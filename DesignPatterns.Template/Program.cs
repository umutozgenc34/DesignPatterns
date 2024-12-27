using BaseProject.Models;
using DesignPatterns.Template.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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
        await userManager.CreateAsync(new AppUser() { UserName = "umut1", Email = "umut1@gmail.com", ImageUrl = "/userimages/user.png" ,Description ="cok iyi bir description"}, "Sifre34.");
        await userManager.CreateAsync(new AppUser() { UserName = "ahmet2", Email = "ahmet2@gmail.com", ImageUrl = "/userimages/user.png", Description = "cok iyi bir description" }, "Sifre34.");
        await userManager.CreateAsync(new AppUser() { UserName = "mehmet3", Email = "mehmet3@gmail.com", ImageUrl = "/userimages/user.png", Description = "cok iyi bir description" }, "Sifre34.");
        await userManager.CreateAsync(new AppUser() { UserName = "faruk4", Email = "faruk4@gmail.com.com", ImageUrl = "/userimages/user.png", Description = "cok iyi bir description" }, "Sifre34.");
        await userManager.CreateAsync(new AppUser() { UserName = "melih5", Email = "melih5@gmail.com", ImageUrl = "/userimages/user.png", Description = "cok iyi bir description" }, "Sifre34.");
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
