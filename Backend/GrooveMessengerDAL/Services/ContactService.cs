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
        public async Task<IEnumerable<IndexUserInfoModel>> GetUserContact(string username = null)
        {
            var currentUser = username == null ? await _userManager.FindByEmailAsync(_userResolverService.CurrentUserName()) : await _userManager.FindByNameAsync(username);
            var currentUserInform = _userInfoRepository.GetBy(x => x.UserId == currentUser.Id.ToString()).FirstOrDefault();
            var contactList = _userInfoContactRepository.GetBy(x => x.UserId == currentUserInform.Id).Include(inc => inc.ContactInfo).Select(x => x.ContactInfo);
            return _mapper.Map<IEnumerable<UserInfoEntity>, IEnumerable<IndexUserInfoModel>>(contactList);
        }

        public async Task<IEnumerable<IndexUserInfoModel>> GetUserUnknownContact(string username = null)
        {
            var currentUser = username == null ? await _userManager.FindByEmailAsync(_userResolverService.CurrentUserName()) : await _userManager.FindByNameAsync(username);
            var currentUserInform = _userInfoRepository.GetBy(x => x.UserId == currentUser.Id.ToString()).FirstOrDefault();
            var currentContactList = _userInfoContactRepository.GetBy(x => x.UserId == currentUserInform.Id).Include(inc => inc.ContactInfo).Select(x => x.ContactInfo);
            var allContacts = _userInfoRepository.GetAll().Where(m => m.UserId != currentUser.Id);
            var unknownContactList = allContacts.Except(currentContactList);// (x => x.UserId == currentUserInform.Id).Include(inc => inc.ContactInfo).Select(x => x.ContactInfo);
            return _mapper.Map<IEnumerable<UserInfoEntity>, IEnumerable<IndexUserInfoModel>>(unknownContactList);
        }

        public async Task DeleteContact(string contactId, string username = null)
        {
            var currentUser = username == null ? await _userManager.FindByEmailAsync(_userResolverService.CurrentUserName()) : await _userManager.FindByNameAsync(username);
            var currentUserInform = _userInfoRepository.GetBy(x => x.UserId == currentUser.Id.ToString()).FirstOrDefault();
            var TableContactId = _userService.GetBy((m) => m.UserId == contactId).FirstOrDefault().Id;
            var currentContact = _userInfoContactRepository.GetBy(x => x.UserId == currentUserInform.Id && x.ContactId == TableContactId).FirstOrDefault();
            currentContact.Deleted = true;
            _userInfoContactRepository.Edit(currentContact);
            _uow.SaveChanges();

        }

        public async Task AddContact(string contactId, string username = null)
        {

            if (username == null) username = _userResolverService.CurrentUserName();
            var currentUser = await _userManager.FindByNameAsync(username);
            ///////////////////////////////////////////////////////////////
            var TableCurrentUserId = _userService.GetBy((m) => m.UserId == currentUser.Id).FirstOrDefault().Id;
            var TableContactId = _userService.GetBy((m) => m.UserId == contactId).FirstOrDefault().Id;



            UserInfoContactEntity newUC = new UserInfoContactEntity()
            {
                UserId = TableCurrentUserId,
                ContactId = TableContactId
            };
            _userInfoContactRepository.Add(newUC);
            _uow.SaveChanges();
        }


    }
}
