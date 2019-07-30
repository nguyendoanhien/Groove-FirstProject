using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using GrooveMessengerDAL.Data;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models;
using GrooveMessengerDAL.Models.User;
using GrooveMessengerDAL.Repositories.Interface;
using GrooveMessengerDAL.Services.Interface;
using GrooveMessengerDAL.Uow.Interface;
using Microsoft.AspNetCore.Identity;

namespace GrooveMessengerDAL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUowBase<GrooveMessengerDbContext> _uow;

        private readonly IGenericRepository<UserInfoContactEntity, Guid, GrooveMessengerDbContext>
            _userInfoContactRepository;

        private readonly IGenericRepository<UserInfoEntity, Guid, GrooveMessengerDbContext> _userRepository;
        public IUserResolverService _userResolverService;

        public UserService(
            IGenericRepository<UserInfoEntity, Guid, GrooveMessengerDbContext> userRepository,
            IMapper mapper,
            IUowBase<GrooveMessengerDbContext> uow,
            UserManager<ApplicationUser> userManager,
            IUserResolverService userResolverService,
            IGenericRepository<UserInfoContactEntity, Guid, GrooveMessengerDbContext> userInformContactRepository
        )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _uow = uow;
            _userManager = userManager;
            _userResolverService = userResolverService;
            _userInfoContactRepository = userInformContactRepository;
        }

        public void AddUserInfo(CreateUserInfoModel userInfo)
        {
            userInfo.Status = "online";
            userInfo.Mood = "";
            userInfo.Avatar = "https://localhost:44383/images/avatar.png";
            var entity = _mapper.Map<CreateUserInfoModel, UserInfoEntity>(userInfo);
            _userRepository.Add(entity);
            _uow.SaveChanges();
        }

        public IQueryable<UserInfoEntity> GetBy(Expression<Func<UserInfoEntity, bool>> predicate)
        {
            var result = _userRepository.GetBy(predicate);
            return result;
        }

        public void Edit(UserInfoEntity entity)
        {
            throw new NotImplementedException();
        }

        public void EditUserInfo(EditUserInfoModel userInfo)
        {
            var storedData = _userRepository.GetSingle(userInfo.Id);
            storedData.DisplayName = userInfo.DisplayName;
            storedData.Avatar = userInfo.Avatar;
            storedData.Mood = userInfo.Mood;
            storedData.Status =
                (UserInfoEntity.StatusName)Enum.Parse(typeof(UserInfoEntity.StatusName), userInfo.Status, true);
            _userRepository.Edit(storedData);
            _uow.SaveChanges();
        }

        public IndexUserInfoModel GetUserInfo(string userId)
        {
            var storedData = _userRepository.FindBy(x => x.UserId == userId).FirstOrDefault();
            var result = _mapper.Map<UserInfoEntity, IndexUserInfoModel>(storedData);
            return result;
        }

        public async Task<IEnumerable<IndexUserInfoModel>> GetAllUserInfo()
        {
            var currentUser = await _userManager.FindByNameAsync(_userResolverService.CurrentUserName());
            var userInformList = _userRepository.GetBy(x => x.UserId != currentUser.Id.ToString());
            return _mapper.Map<IEnumerable<UserInfoEntity>, IEnumerable<IndexUserInfoModel>>(userInformList);
        }

        public string GetPkByUserId(string userId)
        {
            return _userRepository.GetBy(x => x.UserId == userId).FirstOrDefault().Id.ToString();
        }

        public string GetPkByUserId(Guid userId)
        {
            return GetPkByUserId(userId.ToString());
        }

        public async Task<IEnumerable<UserInfoEntity>> GetByUsernameAsync(string username)
        {
            var spName = "[dbo].[usp_UserInfo_GetByUsername]";
            var parameter =
                new SqlParameter
                {
                    ParameterName = "Username",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 256,
                    SqlValue = username
                };

            var contactList =
                _userInfoContactRepository.ExecuteReturedStoredProcedure<UserInfoEntity>(spName, parameter);
            return await Task.FromResult(contactList);
        }

        public UserInfoEntity GetByUsername(string username)
        {
            var userInfo = GetBy(FuncGetByUsername(username)).FirstOrDefault();

            return userInfo;
        }

        public async Task<UserInfoEntity> GetUser(Guid id)
        {
            return await _userRepository.GetSingleAsync(id);
        }
        //Delegate Libraries

        private Expression<Func<UserInfoEntity, bool>> FuncGetByUsername(string username)
        {
            return data => data.ApplicationUser.UserName == username;
        }
    }
}