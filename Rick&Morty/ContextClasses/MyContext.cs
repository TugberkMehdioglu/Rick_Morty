using Microsoft.EntityFrameworkCore;
using Rick_Morty.Entities;

namespace Rick_Morty.ContextClasses
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>().Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Entity<Episode>().Property(x => x.Id).ValueGeneratedNever();

            modelBuilder.Entity<EpisodeCharacters>().HasKey(x => new
            {
                x.CharacterId,
                x.EpisodeId
            });
        }

        public DbSet<Character>? Characters { get; set; }
        public DbSet<Episode>? Episodes { get; set; }
        public DbSet<EpisodeCharacters>? EpisodeCharacters { get; set; }
    }
}
