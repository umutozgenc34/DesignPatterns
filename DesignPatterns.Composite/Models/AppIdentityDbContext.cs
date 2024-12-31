using DesignPatterns.Composite.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Models;

public class AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Book> Books { get; set; }
}
