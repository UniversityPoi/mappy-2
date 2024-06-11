using Mappy.Models;
using Microsoft.EntityFrameworkCore;

namespace Mappy.Repos
{
    public class DefaultDbContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<AccidentModel> Accidents { get; set; }
        
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {
            
        }
    }
}