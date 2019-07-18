﻿using AutoMapper;
using GrooveMessengerDAL.Data;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models;

using GrooveMessengerDAL.Models.User;
using GrooveMessengerDAL.Repositories.Interface;
using GrooveMessengerDAL.Services.Interface;
using GrooveMessengerDAL.Uow.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrooveMessengerDAL.Services
{
    public class ContactService : IContactService
    {
        public IUserResolverService _userResolverService;
        public UserManager<ApplicationUser> _userManager;
        private IUowBase<GrooveMessengerDbContext> _uow;
        private IMapper _mapper;
        private IGenericRepository<UserInfoContactEntity, Guid, GrooveMessengerDbContext> _userInfoContactRepository;
        private IGenericRepository<UserInfoEntity, Guid, GrooveMessengerDbContext> _userInfoRepository;
        private readonly IUserService _userService;
        public ContactService(
            UserManager<ApplicationUser> userManager,
            IUserResolverService userResolverService,
            IUowBase<GrooveMessengerDbContext> uow,
            IMapper mapper,
            IGenericRepository<UserInfoContactEntity, Guid, GrooveMessengerDbContext> userInformContactRepository,
            IGenericRepository<UserInfoEntity, Guid, GrooveMessengerDbContext> userInformRepository,
            IUserService userService
            )
        {
            _userResolverService = userResolverService;
            _uow = uow;
            _userInfoRepository = userInformRepository;
            _mapper = mapper;
            _userInfoContactRepository = userInformContactRepository;
            _userManager = userManager;
            _userService = userService;
        }
        public async Task<IEnumerable<IndexUserInfoModel>> GetAllContact(string username = null)
        {
            var currentUser = username == null ? await _userManager.FindByEmailAsync(_userResolverService.CurrentUserName()) : await _userManager.FindByNameAsync(username);
            var currentUserInform = _userInfoRepository.GetBy(x => x.UserId == currentUser.Id.ToString()).FirstOrDefault();
            var contactList = _userInfoContactRepository.GetBy(x => x.UserId == currentUserInform.Id).Include(inc => inc.ContactInfo).Select(x => x.ContactInfo);
            return _mapper.Map<IEnumerable<UserInfoEntity>, IEnumerable<IndexUserInfoModel>>(contactList);
        } // Hien rem hau

        //IEnumerable<IndexUserInfoModel> IContactService.GetAllContact(string username)
        //{
        //    throw new NotImplementedException();
        //}
        //public  IEnumerable<IndexUserInfoModel> GetAllContact(string username = null)
        //{
        //    if (username == null) username = _userResolverService.CurrentUserName();
        //    var currentUserInform = _userService.GetUserInfo(username);
        //    var contactList = _userInfoContactRepository.GetBy(x => x.UserId == currentUserInform.Id).Include(inc => inc.ContactInfo).Select(x => x.ContactInfo);
        //    return _mapper.Map<IEnumerable<UserInfoEntity>, IEnumerable<IndexUserInfoModel>>(contactList);
        //}
    }
}
