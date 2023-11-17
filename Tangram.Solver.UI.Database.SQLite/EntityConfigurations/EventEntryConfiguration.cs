using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tangram.Solver.UI.Database.SQLite.Entries;

namespace Tangram.Solver.UI.Database.SQLite.EntityConfigurations
{
    public class EventEntryConfiguration : IEntityTypeConfiguration<EventEntry>
    {
        public void Configure(EntityTypeBuilder<EventEntry> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
