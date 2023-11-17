using AutoMapper;
using Tangram.Solver.UI.Database.SQLite.Entries;
using Tangram.Solver.UI.Domain;

namespace Tangram.Solver.UI.Database.SQLite.Extensions
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<GiftEvent, EventEntry>()
                .ReverseMap();

            CreateMap<Participant, ParticipantEntry>()
                .ReverseMap();

            CreateMap<EventEntry, EventEntry>();
            CreateMap<ParticipantEntry, ParticipantEntry>();
        }
    }
}
