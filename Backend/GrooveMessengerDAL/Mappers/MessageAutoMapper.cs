using AutoMapper;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrooveMessengerDAL.Mappers
{
    public class MessageAutoMapperProfile: Profile
    {
        public MessageAutoMapperProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<FullMessageModel, MessageEntity>();
            CreateMap<MessageEntity, FullMessageModel>();
            CreateMap<CreateMessageModel, MessageEntity>();
            CreateMap<MessageEntity, EditMessageModel>();
        }
    }
}
