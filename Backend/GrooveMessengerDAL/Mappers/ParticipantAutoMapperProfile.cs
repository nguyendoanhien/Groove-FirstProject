using AutoMapper;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.Participant;

namespace GrooveMessengerDAL.Mappers
{
    public class ParticipantAutoMapperProfile : Profile
    {
        public ParticipantAutoMapperProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<ParticipantModel, ParticipantEntity>();
        }
    }
}