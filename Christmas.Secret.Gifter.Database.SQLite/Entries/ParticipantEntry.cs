using System.ComponentModel.DataAnnotations.Schema;

namespace Christmas.Secret.Gifter.Database.SQLite.Entries
{
    public class ParticipantEntry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(2400)")]
        public int[] ExcludedOrderIds { get; set; }

        public string EventId { get; set; }
        public EventEntry Event { get; set; }
    }
}
