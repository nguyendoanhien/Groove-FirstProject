using AutoMapper;
using GrooveMessengerDAL.Data;
using GrooveMessengerDAL.Models;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.User;
using GrooveMessengerDAL.Repositories.Interface;
using GrooveMessengerDAL.Services.Interface;
using GrooveMessengerDAL.Uow.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace GrooveMessengerDAL.Services
{
    public class UserService : IUserService
    {
        private IGenericRepository<UserInfoEntity, Guid, GrooveMessengerDbContext> _userRepository;
        private IMapper _mapper;
        private IUowBase<GrooveMessengerDbContext> _uow;

        public UserService(
            IGenericRepository<UserInfoEntity, Guid, GrooveMessengerDbContext> userRepository,
            IMapper mapper,
            IUowBase<GrooveMessengerDbContext> uow
            )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public void AddUserInfo(CreateUserInfoModel userInfo)
        {
            try
            {


                var entity = _mapper.Map<CreateUserInfoModel, UserInfoEntity>(userInfo);
                entity.CreatedOn = DateTime.Now;
                entity.CreatedBy = "Root";
                entity.Status = 0;
                entity.Id = new Guid();
                entity.DisplayName = entity.DisplayName ?? "Test";
                _userRepository.Add(entity);
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public IQueryable<UserInfoEntity> GetBy(Expression<Func<UserInfoEntity, bool>> predicate)
        {
            IQueryable<UserInfoEntity> result = _userRepository.GetBy(predicate);
            return result;
        }

        public async Task<UserInfoEntity> GetUser(Guid id)
        {
            return await _userRepository.GetSingleAsync(id);


        }
    }
}
