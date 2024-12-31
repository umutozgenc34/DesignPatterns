using BaseProject.Models;
using DesignPatterns.Composite.Models;
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
        var newUser = new AppUser() { UserName = "umut1", Email = "umut1@gmail.com" };
        userManager.CreateAsync(newUser, "Sifre34.").Wait();

        await userManager.CreateAsync(new AppUser() { UserName = "ahmet2", Email = "ahmet2@gmail.com" }, "Sifre34.");
        await userManager.CreateAsync(new AppUser() { UserName = "mehmet3", Email = "mehmet3@gmail.com" }, "Sifre34.");
        await userManager.CreateAsync(new AppUser() { UserName = "faruk4", Email = "faruk4@gmail.com.com" }, "Sifre34.");
        await userManager.CreateAsync(new AppUser() { UserName = "melih5", Email = "melih5@gmail.com" }, "Sifre34.");

        var newCategory1 = new Category { Name = "Suç", ReferenceId = 0, UserId = newUser.Id };
        var newCategory2 = new Category { Name = "Cinayet", ReferenceId = 0, UserId = newUser.Id };
        var newCategory3 = new Category { Name = "Polisiye", ReferenceId = 0, UserId = newUser.Id };

        identityDbContext.Categories.AddRange(newCategory1, newCategory2, newCategory3);

        identityDbContext.SaveChanges();

        var subCategory1 = new Category { Name = "Suç 1", ReferenceId = newCategory1.Id, UserId = newUser.Id };
        var subCategory2 = new Category { Name = "Cinayet 1", ReferenceId = newCategory2.Id, UserId = newUser.Id };
        var subCategory3 = new Category { Name = "Polisiye 1", ReferenceId = newCategory3.Id, UserId = newUser.Id };

        identityDbContext.Categories.AddRange(subCategory1, subCategory2, subCategory3);
        identityDbContext.SaveChanges();

        var subCategory4 = new Category { Name = "Cinayet 1.1", ReferenceId = subCategory2.Id, UserId = newUser.Id };

        identityDbContext.Categories.Add(subCategory4);
        identityDbContext.SaveChanges();
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
