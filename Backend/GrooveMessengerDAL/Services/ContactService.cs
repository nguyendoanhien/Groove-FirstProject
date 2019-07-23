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
using System.Data.SqlClient;
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

        private readonly IUserService _userService;

        public ContactService(
            UserManager<ApplicationUser> userManager,
            IUserResolverService userResolverService,
            IUowBase<GrooveMessengerDbContext> uow,
            IMapper mapper,
            IGenericRepository<UserInfoContactEntity, Guid, GrooveMessengerDbContext> userInformContactRepository,
            IGenericRepository<UserInfoEntity, Guid, GrooveMessengerDbContext> userInformRepository,
            IGenericRepository<ParticipantEntity, Guid, GrooveMessengerDbContext> parRepository,
            IGenericRepository<MessageEntity, Guid, GrooveMessengerDbContext> mesgRepository,
            IUserService userService
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

            _userService = userService;
        }
        public async Task<IEnumerable<IndexUserInfoModel>> GetUserContactList(string username = null)
        {
            // Not good as connecting to database to get data three times
            // Replace by calling Stored Procedure

            //var currentUser = username == null ? await _userManager.FindByEmailAsync(_userResolverService.CurrentUserName()) : await _userManager.FindByNameAsync(username);
            //var currentUserInform = _userInfoRepository.GetBy(x => x.UserId == currentUser.Id.ToString()).FirstOrDefault();
            //var contactList = _userInfoContactRepository.GetBy(x => x.UserId == currentUserInform.Id).Include(inc => inc.ContactInfo).Select(x => x.ContactInfo);

            var spName = "[dbo].[usp_GetUserContactList]";
            var parameter =
                new SqlParameter
                {
                    ParameterName = "UserInfoId",
                    SqlDbType = System.Data.SqlDbType.UniqueIdentifier,
                    SqlValue = string.IsNullOrEmpty(username) ? _userResolverService.CurrentUserInfoId() : username
                };

            var contactList = _userInfoContactRepository.ExecuteReturedStoredProcedure<IndexUserInfoModel>(spName, parameter);
            return contactList;
        }

        public async Task<IEnumerable<string>> GetUserContactEmailList(string username = null)
        {
            var spName = "[dbo].[usp_GetUserContactEmailList]";
            var parameter =
                new SqlParameter
                {
                    ParameterName = "UserInfoId",
                    SqlDbType = System.Data.SqlDbType.UniqueIdentifier,
                    SqlValue = string.IsNullOrEmpty(username) ? _userResolverService.CurrentUserInfoId() : username
                };

            var contactList = _userInfoContactRepository.ExecuteReturedStoredProcedure<string>(spName, parameter);
            return contactList;
        }
        //public async Task<List<ContactLatestChatListModel>> GetLatestContactChatListByUserId()
        //{
        //    List<ContactLatestChatListModel> contactList = new List<ContactLatestChatListModel> { };

        //    var currentUser = await _userManager.FindByEmailAsync(_userResolverService.CurrentUserName());
        //    var convOfCurrentUser = _parRepository.GetBy(x => x.UserId == currentUser.Id.ToString()).Include(inc => inc.ConversationEntity).Select(x => x.ConversationEntity).ToList();
        //    foreach (var item in convOfCurrentUser)
        //    {
        //        var contactOfCurrentUser = _parRepository.GetBy(x => x.UserId != currentUser.Id.ToString() && x.ConversationId == item.Id).FirstOrDefault();
        //        var convLastestMessage = _mesgRepository.GetBy(x => x.ConversationId == item.Id).OrderByDescending(x => x.Id).FirstOrDefault();
        //        var userContactInfo = _userInfoRepository.GetBy(x => x.UserId == contactOfCurrentUser.UserId).SingleOrDefault();
        //        contactList.Add(new ContactLatestChatListModel()
        //        {
        //            ConvId = item.Id.ToString(),
        //            ContactId = contactOfCurrentUser.UserId,
        //            DisplayName = userContactInfo.DisplayName,
        //            LastMessage = convLastestMessage.Content,
        //            LastMessageTime = convLastestMessage.CreatedOn
        //        });
        //    }
        //    return contactList;
        //}

        public async Task<IEnumerable<IndexUserInfoModel>> GetUserUnknownContact(string username = null)
        {
            var currentUser = username == null ? await _userManager.FindByEmailAsync(_userResolverService.CurrentUserName()) : await _userManager.FindByNameAsync(username);
            var currentUserInform = _userInfoRepository.GetBy(x => x.UserId == currentUser.Id.ToString()).FirstOrDefault();
            var currentContactList = _userInfoContactRepository.GetBy(x => x.UserId == currentUserInform.Id).Include(inc => inc.ContactInfo).Select(x => x.ContactInfo);
            var allContacts = _userInfoRepository.GetAll().Where(m => m.UserId != currentUser.Id);
            var unknownContactList = allContacts.Except(currentContactList);// (x => x.UserId == currentUserInform.Id).Include(inc => inc.ContactInfo).Select(x => x.ContactInfo);
            return _mapper.Map<IEnumerable<UserInfoEntity>, IEnumerable<IndexUserInfoModel>>(unknownContactList);
        }

        public void DeleteContact(DeleteContactModel deleteContactModel)
        {
            var getContact = _userInfoContactRepository.GetBy(m => m.UserId == new Guid(deleteContactModel.UserId) && m.ContactId == new Guid(deleteContactModel.ContactId)).FirstOrDefault();
            //getContact = _mapper.Map<DeleteContactModel, UserInfoContactEntity>(deleteContactModel);
            getContact.Deleted = true;
            _userInfoContactRepository.Edit(getContact);
            _uow.SaveChanges();

        }

        public void AddContact(AddContactModel addContactModel)
        {
            var newUC = _mapper.Map<AddContactModel, UserInfoContactEntity>(addContactModel);
            _userInfoContactRepository.Add(newUC);
            _uow.SaveChanges();
        }
        public void EditContact(EditContactModel editContactModel)
        {
            var getContact = _userInfoContactRepository.GetBy(m => m.UserId == new Guid(editContactModel.UserId) && m.ContactId == new Guid(editContactModel.ContactId)).FirstOrDefault();
            getContact.NickName = editContactModel.NickName;
            _userInfoContactRepository.Add(getContact);
            _uow.SaveChanges();
        }

        public Task<IEnumerable<IndexUserInfoModel>> GetUserContact(string username = null)
        {
            throw new NotImplementedException();
        }

        public List<ContactLatestChatListModel> GetLatestContactChatListByUserId()
        {
            var spName = "[dbo].[usp_GetLatestContactChatListByUserId]";
            var parameter =
                new SqlParameter
                {
                    ParameterName = "UserId",
                    SqlDbType = System.Data.SqlDbType.UniqueIdentifier,
                    SqlValue = "143ee0d6-0443-4132-a3fc-19af34728daa"
                    //_userResolverService.CurrentUserInfoId()
                };

            var contactListDraft = _userInfoRepository.ExecuteReturedStoredProcedure<ContactLatestChatListModel>(spName, parameter);

            List<ContactLatestChatListModel> latestChatListModels = new List<ContactLatestChatListModel>();
            var contactChatBox = from contact in contactListDraft
                                 group contact by new { contact.ContactId, contact.ConvId, contact.DisplayName } into ContactChat
                                 orderby ContactChat.Key.ContactId, ContactChat.Key.DisplayName, ContactChat.Key.ConvId
                                 select new
                                 {
                                     ConvId = ContactChat.Key.ConvId,
                                     ContactId = ContactChat.Key.ContactId,
                                     DisplayName = ContactChat.Key.DisplayName,
                                     LastMessage = ContactChat.OrderByDescending(x => x.LastMessage).FirstOrDefault().LastMessage,
                                     LastMessageTime = ContactChat.OrderByDescending(x => x.LastMessageTime).FirstOrDefault().LastMessageTime
                                 };
            foreach (var item in contactChatBox)
            {
                if (item.ContactId != _userResolverService.CurrentUserInfoId())
                {
                    ContactLatestChatListModel contactLatestChatListModel = new ContactLatestChatListModel()
                    {
                        ConvId = item.ConvId,
                        ContactId = item.ContactId,
                        DisplayName = item.DisplayName,
                        LastMessage = item.LastMessage,
                        LastMessageTime = item.LastMessageTime
                    };
                    latestChatListModels.Add(contactLatestChatListModel);
                }
            }
            return latestChatListModels;
        }
    }
}
