using Microsoft.EntityFrameworkCore;
using Ms.Services.ShoppingCartAPI.Models;
using MS.Services.ShoppingCartAPI.Models;

namespace MS.Services.ShoppingCartAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<CartHeader> CartHeaders { get; set; }

       

           
    }
}
