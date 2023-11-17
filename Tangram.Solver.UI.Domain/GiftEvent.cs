using TypeGen.Core.TypeAnnotations;

namespace Tangram.Solver.UI.Domain
{
    [ExportTsInterface]
    public class GiftEvent
    {
        public string Id { get; set; } = null!;
        public int OrginizerId { get; set; }
        public EventState State { get; set; }
        public GameSettings GameSettings { get; set; }
        public IEnumerable<Participant> Participants { get; set; } = new List<Participant>();
    }
}