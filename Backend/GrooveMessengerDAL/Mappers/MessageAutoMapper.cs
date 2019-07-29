using AutoMapper;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.Message;

namespace GrooveMessengerDAL.Mappers
{
    public class MessageAutoMapperProfile : Profile
    {
        public MessageAutoMapperProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<FullMessageModel, MessageEntity>();
            CreateMap<MessageEntity, FullMessageModel>();
            CreateMap<CreateMessageModel, MessageEntity>().ForMember(x => x.ApplicationUser, opt => opt.Ignore())
                .ForMember(x => x.ConversationEntity, opt => opt.Ignore());
            CreateMap<MessageEntity, EditMessageModel>();
            CreateMap<MessageEntity, IndexMessageModel>();
        }
    }
}