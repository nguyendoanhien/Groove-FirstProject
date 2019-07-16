using GrooveMessengerDAL.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrooveMessengerDAL.Services.Interface
{
    public interface IUserService
    {
        void AddUserInfo(CreateUserInfoModel userInfo);
    }
}
