using Mappy.Models;
using Microsoft.EntityFrameworkCore;

namespace Mappy.Repos
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<AccidentModel> Accidents { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
    }
}