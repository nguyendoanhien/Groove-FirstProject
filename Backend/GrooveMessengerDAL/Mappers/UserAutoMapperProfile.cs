using AutoMapper;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.User;

namespace GrooveMessengerDAL.Mappers
{
    public class UserAutoMapperProfile : Profile
    {
        public UserAutoMapperProfile()
        {
            CreateMap<UserInfoEntity, IndexUserInfoModel>();
            CreateMap<EditUserInfoModel, UserInfoEntity>();
            CreateMap<CreateUserInfoModel, UserInfoEntity>();
        }
    }
}