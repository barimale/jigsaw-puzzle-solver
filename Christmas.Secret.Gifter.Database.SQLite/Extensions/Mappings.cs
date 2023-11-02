using AutoMapper;
using Christmas.Secret.Gifter.Database.SQLite.Entries;
using Christmas.Secret.Gifter.Domain;

namespace Christmas.Secret.Gifter.Database.SQLite.Extensions
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
