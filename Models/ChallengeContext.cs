using Microsoft.EntityFrameworkCore;

namespace Challenge.Models
{
    public class ChallengeContext : DbContext
    {
        public ChallengeContext (DbContextOptions<ChallengeContext> options)
            : base(options)
        {
        }

        public DbSet<History> Operations { get; set; }
        public DbSet<Client>  Clients { get; set; }
    }
}
