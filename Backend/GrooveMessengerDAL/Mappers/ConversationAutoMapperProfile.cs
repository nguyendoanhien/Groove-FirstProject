using AutoMapper;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.Conversation;

namespace GrooveMessengerDAL.Mappers
{
    public class ConversationAutoMapperProfile : Profile
    {
        public ConversationAutoMapperProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<CreateConversationModel, ConversationEntity>();
            CreateMap<ConversationEntity, IndexConversationModel>();
        }
    }
}