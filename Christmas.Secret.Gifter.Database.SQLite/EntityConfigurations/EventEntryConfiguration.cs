using Christmas.Secret.Gifter.Database.SQLite.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Christmas.Secret.Gifter.Database.SQLite.SQLite.Database.Configuration
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
