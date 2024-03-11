using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace LeagueApi.Model
{
    public class AppDb:DbContext
    {
        public AppDb(DbContextOptions<AppDb> options):base(options)
        {
            
        }
        public DbSet <League> Leagues   { get; set; }
        public DbSet <Group>  Groups { get; set; }
        public DbSet <Team>  Teams { get; set; }
        public DbSet <Match>  Matches { get; set; }
        public DbSet <Result>  Results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<League>().HasData(new League { Lid = 1, Name = "First League", state = 0 });
            base.OnModelCreating(modelBuilder);
        }


    }
}
