using Bookstore.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
namespace Bookstore.DataAccess.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Name = "SciFi",
                DisplayOrder = 1
            },
            new Category
            {
                Id = 2,
                Name = "Novel",
                DisplayOrder = 2
            },
            new Category
            {
                Id = 3,
                Name = "Thriller",
                DisplayOrder = 3
            }
            ) ;
    }
}
