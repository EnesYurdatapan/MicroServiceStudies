using Microsoft.EntityFrameworkCore;
using Ms.Services.EmailAPI.Models;

namespace MS.Services.EmailAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<EmailLogger> EmailLoggers { get; set; }
    }
}
