using AutoMapper;
using GrooveMessengerDAL.Data;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models;
using GrooveMessengerDAL.Models.Contact;
using GrooveMessengerDAL.Models.CustomModel;
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
        private IGenericRepository<ParticipantEntity, Guid, GrooveMessengerDbContext> _parRepository;
        private IGenericRepository<MessageEntity, Guid, GrooveMessengerDbContext> _mesgRepository;
        public ContactService(
            UserManager<ApplicationUser> userManager,
            IUserResolverService userResolverService,
            IUowBase<GrooveMessengerDbContext> uow,
            IMapper mapper,
            IGenericRepository<UserInfoContactEntity, Guid, GrooveMessengerDbContext> userInformContactRepository,
            IGenericRepository<UserInfoEntity, Guid, GrooveMessengerDbContext> userInformRepository,
            IGenericRepository<ParticipantEntity, Guid, GrooveMessengerDbContext> parRepository,
            IGenericRepository<MessageEntity, Guid, GrooveMessengerDbContext> mesgRepository
            )
        {
            _userResolverService = userResolverService;
            _uow = uow;
            _userInfoRepository = userInformRepository;
            _mapper = mapper;
            _userInfoContactRepository = userInformContactRepository;
            _userManager = userManager;
            _parRepository = parRepository;
            _mesgRepository = mesgRepository;
        }
        public async Task<IEnumerable<IndexUserInfoModel>> GetAllContact(string username = null)
        {
            var currentUser = username == null ? await _userManager.FindByEmailAsync(_userResolverService.CurrentUserName()) : await _userManager.FindByNameAsync(username);
            var currentUserInform = _userInfoRepository.GetBy(x => x.UserId == currentUser.Id.ToString()).FirstOrDefault();
            var contactList = _userInfoContactRepository.GetBy(x => x.UserId == currentUserInform.Id).Include(inc => inc.ContactInfo).Select(x => x.ContactInfo);
            return _mapper.Map<IEnumerable<UserInfoEntity>, IEnumerable<IndexUserInfoModel>>(contactList);
        }
        public async Task<List<ContactLatestChatListModel>> GetLatestContactChatListByUserId()
        {
            List<ContactLatestChatListModel> contactList = new List<ContactLatestChatListModel> { };

            var currentUser = await _userManager.FindByEmailAsync(_userResolverService.CurrentUserName());
            var convOfCurrentUser = _parRepository.GetBy(x => x.UserId == currentUser.Id.ToString()).Include(inc => inc.ConversationEntity).Select(x => x.ConversationEntity).ToList();
            foreach (var item in convOfCurrentUser)
            {
                var contactOfCurrentUser = _parRepository.GetBy(x => x.UserId != currentUser.Id.ToString() && x.ConversationId == item.Id).FirstOrDefault();
                var convLastestMessage = _mesgRepository.GetBy(x => x.ConversationId == item.Id).OrderByDescending(x => x.Id).FirstOrDefault();
                var userContactInfo = _userInfoRepository.GetBy(x => x.UserId == contactOfCurrentUser.UserId).SingleOrDefault();
                contactList.Add(new ContactLatestChatListModel()
                {
                    ConvId = item.Id.ToString(),
                    ContactId = contactOfCurrentUser.UserId,
                    DisplayName = userContactInfo.DisplayName,
                    LastMessage = convLastestMessage.Content,
                    LastMessageTime = convLastestMessage.CreatedOn
                });
            }
            return contactList;
        }
        //public IQueryable<FullContactModel> GetFromUsername(string userName)
        //{

        //    var res = _userInfoContactRepository.GetBy((m) => m.UserInfo.ApplicationUser.UserName == userName);
        //    var reRes = _mapper.Map<IQueryable<UserInfoContactEntity>, IQueryable<FullContactModel>>(res);
        //    return reRes;
        //}

        //public IQueryable<FullContactModel> GetContacts()
        //{
        //    var res = _userInfoContactRepository.GetAll();
        //    var reRes = _mapper.Map<IQueryable<UserInfoContactEntity>, IQueryable<FullContactModel>>(res);
        //    return reRes;
        //}
    }
}
