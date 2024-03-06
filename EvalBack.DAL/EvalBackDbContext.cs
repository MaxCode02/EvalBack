using EvalBack.Entity;
using Microsoft.EntityFrameworkCore;


namespace EvalBack.DAL
{
    public class EvalBackDbContext : DbContext
    {
        public EvalBackDbContext(DbContextOptions<EvalBackDbContext> options) : base(options) { }

        public DbSet<Event> Events { get; set; }

    }
}