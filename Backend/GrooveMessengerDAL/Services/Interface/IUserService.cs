using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.User;

namespace GrooveMessengerDAL.Services.Interface
{
    public interface IUserService
    {
        void AddUserInfo(CreateUserInfoModel userInfo);

        IQueryable<UserInfoEntity> GetBy(Expression<Func<UserInfoEntity, bool>> predicate);

        void Edit(UserInfoEntity entity);

        void EditUserInfo(EditUserInfoModel userInfo);

        IndexUserInfoModel GetUserInfo(string userId);

        Task<IEnumerable<IndexUserInfoModel>> GetAllUserInfo();

        string GetPkByUserId(string userId);

        string GetPkByUserId(Guid userId);
    }
}