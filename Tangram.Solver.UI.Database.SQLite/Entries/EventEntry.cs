using System.ComponentModel.DataAnnotations.Schema;
using Tangram.Solver.UI.Domain;

namespace Tangram.Solver.UI.Database.SQLite.Entries
{
    public class EventEntry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = null!;
        public int OrganizerId { get; set; }
        public EventState State { get; set; }
        public List<ParticipantEntry> Participants { get; set; } = new List<ParticipantEntry>();
    }
}
