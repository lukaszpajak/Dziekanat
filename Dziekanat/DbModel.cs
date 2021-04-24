using Microsoft.EntityFrameworkCore;

namespace Dziekanat
{
    public class DbModel : DbContext
    {

        public DbSet<Student> Student { get; set; }
        public DbSet<Grade> Grade { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=DziekanatDB;Trusted_Connection=True; ");

        }


    }
}
