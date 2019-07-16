
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models;
using GrooveMessengerDAL.Models.User;
using System.Threading.Tasks;

namespace GrooveMessengerDAL.Services.Interface
{
    public interface IUserService
    {
        Task<UserInfoEntity> GetUser(System.Guid id);
        void AddUserInfo(CreateUserInfoModel userInfo);
    }
}
