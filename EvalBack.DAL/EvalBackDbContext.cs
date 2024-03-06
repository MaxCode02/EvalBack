using Microsoft.EntityFrameworkCore;


namespace EvalBack.DAL
{
    public class EvalBackDbContext : DbContext
    {
        public EvalBackDbContext(DbContextOptions<EvalBackDbContext> options) : base(options) { }

    }
}