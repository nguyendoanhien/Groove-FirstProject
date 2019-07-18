
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models;
using GrooveMessengerDAL.Models.User;
using GrooveMessengerDAL.Repositories.Interface;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GrooveMessengerDAL.Services.Interface
{
    public interface IUserService
    {
        void AddUserInfo(CreateUserInfoModel userInfo);
        void EditUserInfo(EditUserInfoModel userInfo);
        IndexUserInfoModel GetUserInfo(string id);
    }
}
