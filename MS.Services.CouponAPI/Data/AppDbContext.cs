using Microsoft.EntityFrameworkCore;
using MS.Services.CouponAPI.Models;

namespace MS.Services.CouponAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 1,
                CouponCode = "10FF",
                DiscountAmount = 10,
                MinAmount = 20,
            },
            new Coupon
            {
                CouponId = 2,
                CouponCode = "20FF",
                DiscountAmount = 20,
                MinAmount = 400,
            }
            );
        }
    }
}
