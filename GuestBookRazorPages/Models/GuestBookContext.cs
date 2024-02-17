using GuestBookRazorPages.Models.AccountModels;
using Microsoft.EntityFrameworkCore;

namespace GuestBookRazorPages.Models
{
    public class GuestBookContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Message> Messages { get; set; }

        public GuestBookContext(DbContextOptions<GuestBookContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
