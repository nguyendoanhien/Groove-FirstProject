﻿using AutoMapper;
using GrooveMessengerDAL.Data;
using GrooveMessengerDAL.Models;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.User;
using GrooveMessengerDAL.Repositories.Interface;
using GrooveMessengerDAL.Services.Interface;
using GrooveMessengerDAL.Uow.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;

namespace GrooveMessengerDAL.Services
{
    public class UserService : IUserService
    {
        private IGenericRepository<UserInfoEntity, Guid, GrooveMessengerDbContext> _userRepository;
        private IMapper _mapper;
        private IUowBase<GrooveMessengerDbContext> _uow;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(
            IGenericRepository<UserInfoEntity, Guid, GrooveMessengerDbContext> userRepository,
            IMapper mapper,
            IUowBase<GrooveMessengerDbContext> uow,
            UserManager<ApplicationUser> userManager
            )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _uow = uow;
            _userManager = userManager;
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


        public void EditUserInfo(EditUserInfoModel userInfo)
        {
            var storedData = _userRepository.GetSingle(userInfo.Id);
            storedData.DisplayName = userInfo.DisplayName;
            storedData.Avatar = userInfo.Avatar;
            storedData.Mood = userInfo.Mood;
            storedData.Status = (UserInfoEntity.StatusName)Enum.Parse(typeof(UserInfoEntity.StatusName), userInfo.Status, true);
            _userRepository.Edit(storedData);
            _uow.SaveChanges();
        }

        public IndexUserInfoModel GetUserInfo(string id)
        {
            var storedData = _userRepository.FindBy(x=>x.UserId == id).FirstOrDefault();
            var result = _mapper.Map<UserInfoEntity, IndexUserInfoModel>(storedData);
            return result;
        }
    }
}
