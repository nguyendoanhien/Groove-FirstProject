
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
        Task<UserInfoEntity> GetUser(System.Guid id);
        void AddUserInfo(CreateUserInfoModel userInfo);
        IQueryable<UserInfoEntity> GetBy(Expression<Func<UserInfoEntity, bool>> predicate);
        Task<UserInfoEntity> GetByUsername(string username);
        void Edit(UserInfoEntity entity);
        Task EditAsync(EditUserInfoModel editUserInfoModel);
    }
}
