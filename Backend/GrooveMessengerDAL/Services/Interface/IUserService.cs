
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

        IQueryable<UserInfoEntity> GetBy(Expression<Func<UserInfoEntity, bool>> predicate);
      
        void Edit(UserInfoEntity entity);
     
        void EditUserInfo(EditUserInfoModel userInfo);

        IndexUserInfoModel GetUserInfo(string userId);
        string GetPkByUserId(string userId);
        string GetPkByUserId(Guid userId);
    }
}
