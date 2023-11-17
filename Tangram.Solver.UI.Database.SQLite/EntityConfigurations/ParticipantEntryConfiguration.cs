using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tangram.Solver.UI.Database.SQLite.Entries;

namespace Tangram.Solver.UI.Database.SQLite.EntityConfigurations
{
    public class ParticipantEntryConfiguration : IEntityTypeConfiguration<ParticipantEntry>
    {
        public void Configure(EntityTypeBuilder<ParticipantEntry> builder)
        {
            var converter = new ValueConverter<int[], string>(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.TrimEntries).Select(p => int.Parse(p)).ToArray());

            builder.HasKey(o => o.Id);
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasOne(p => p.Event)
                .WithMany(pp => pp.Participants)
                .HasForeignKey(ppp => ppp.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(p => p.ExcludedOrderIds)
                .HasConversion(converter);
        }
    }
}
