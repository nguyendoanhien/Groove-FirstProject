using AutoMapper;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.User;

namespace GrooveMessengerDAL.Mappers
{
    public class UserInformAutoMapperProfile : Profile
    {
        public UserInformAutoMapperProfile()
        {
            CreateMap<UserInfoEntity, IndexUserInfoModel>();
        }
    }
}
