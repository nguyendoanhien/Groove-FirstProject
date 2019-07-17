using AutoMapper;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrooveMessengerDAL.Mappers
{
    public class MessageAutoMapperProfile : Profile
    {
        public MessageAutoMapperProfile()
        {
            CreateMap<MessageEntity, EditMessageModel>();
        }
    }
}
