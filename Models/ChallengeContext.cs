using Microsoft.EntityFrameworkCore;

namespace Challenge.Models
{
    public class ChallengeContext : DbContext
    {
        public ChallengeContext (DbContextOptions<ChallengeContext> options)
            : base(options)
        {
        }

        public DbSet<Operation> Operations { get; set; }
        public DbSet<Client>  Clients { get; set; }
    }
}
