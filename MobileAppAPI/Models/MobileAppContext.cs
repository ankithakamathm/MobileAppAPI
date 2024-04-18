using Microsoft.EntityFrameworkCore;

namespace MobileAppAPI.Models
{
    public class MobileAppContext : DbContext
    {
        

        public MobileAppContext(DbContextOptions<MobileAppContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
    
}
}
