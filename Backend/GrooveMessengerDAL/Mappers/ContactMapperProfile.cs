using AutoMapper;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.Contact;

namespace GrooveMessengerDAL.Mappers
{
    public class ContactMapperProfile : Profile
    {
        public ContactMapperProfile()
        {
            CreateMap<DeleteContactModel, UserInfoContactEntity>();
            CreateMap<EditContactModel, UserInfoContactEntity>();
            CreateMap<AddContactModel, UserInfoContactEntity>();
        }
    }
}