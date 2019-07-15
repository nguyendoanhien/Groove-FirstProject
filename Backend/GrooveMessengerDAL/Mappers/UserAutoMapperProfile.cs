using AutoMapper;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrooveMessengerDAL.Mappers
{
    public class UserAutoMapperProfile: Profile
    {
        public UserAutoMapperProfile()
        {
            CreateMap<EditUserInfoModel, UserInfoEntity>();
        }
    }
}
