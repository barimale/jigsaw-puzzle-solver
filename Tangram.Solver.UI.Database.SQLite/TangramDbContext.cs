using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Tangram.Solver.UI.Database.SQLite.Entries;

namespace Tangram.Solver.UI.Database.SQLite
{
    public sealed class TangramDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public TangramDbContext(DbContextOptions<TangramDbContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<EventEntry> Events { get; set; }
        public DbSet<ParticipantEntry> Participants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
