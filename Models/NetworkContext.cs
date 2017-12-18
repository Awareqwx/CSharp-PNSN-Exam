using Microsoft.EntityFrameworkCore;
 
namespace ProfessionalNetwork.Models
{
    public class NetworkContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public NetworkContext(DbContextOptions<NetworkContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Connection> Connections { get; set; }
    }
}