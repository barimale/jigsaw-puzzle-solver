using Christmas.Secret.Gifter.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Christmas.Secret.Gifter.Database.SQLite.Entries
{
    public class GameSettingsEntry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;
        public int EventId { get; set; }
        public GameSettings GameSettings { get; set; }
        public List<ParticipantEntry> Participants { get; set; } = new List<ParticipantEntry>();
    }
}
