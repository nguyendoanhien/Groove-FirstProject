using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
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

namespace GrooveMessengerDAL.Services
{
    public class ContactService : IContactService
    {
        private readonly IUserService _userService;
        private IMapper _mapper;
        private IGenericRepository<MessageEntity, Guid, GrooveMessengerDbContext> _mesgRepository;

        private readonly IGenericRepository<ParticipantEntity, Guid, GrooveMessengerDbContext> _parRepository;
        private IUowBase<GrooveMessengerDbContext> _uow;

        private readonly IGenericRepository<UserInfoContactEntity, Guid, GrooveMessengerDbContext>
            _userInfoContactRepository;

        private readonly IGenericRepository<UserInfoEntity, Guid, GrooveMessengerDbContext> _userInfoRepository;
        public UserManager<ApplicationUser> UserManager;
        public IUserResolverService UserResolverService;

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
            UserResolverService = userResolverService;
            _uow = uow;
            _userInfoRepository = userInformRepository;
            _mapper = mapper;
            _userInfoContactRepository = userInformContactRepository;
            UserManager = userManager;

            _parRepository = parRepository;
            _mesgRepository = mesgRepository;

            _userService = userService;
        }

        public async Task<IEnumerable<IndexUserInfoModel>> GetUserContactList(string username = null)
        {
            var spName = "[dbo].[usp_GetUserContactList]";
            var parameter =
                new SqlParameter
                {
                    ParameterName = "UserInfoId",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    SqlValue = string.IsNullOrEmpty(username) ? UserResolverService.CurrentUserInfoId() : username
                };

            var contactList =
                _userInfoContactRepository.ExecuteReturedStoredProcedure<IndexUserInfoModel>(spName, parameter);
            return await Task.FromResult(contactList);
        }


        public async Task<IEnumerable<string>> GetUserContactEmailList(string username = null)
        {
            var spName = "[dbo].[usp_GetUserContactEmailList]";
            var parameter =
                new SqlParameter
                {
                    ParameterName = "UserInfoId",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    SqlValue = string.IsNullOrEmpty(username) ? UserResolverService.CurrentUserInfoId() : username
                };

            var contactList = _userInfoContactRepository.ExecuteReturedStoredProcedure<string>(spName, parameter);
            return await Task.FromResult(contactList);
        }

        public async Task<string> GetUserContactEmail(string userId)
        {
            var email = await _userInfoRepository.FindBy(x => x.UserId == userId).Include(x => x.ApplicationUser)
                .Select(x => x.ApplicationUser.Email).FirstAsync();
            return email;
        }
        //public async Task<List<ContactLatestChatListModel>> GetLatestContactChatListByUserId()
        //{
        //    List<ContactLatestChatListModel> contactList = new List<ContactLatestChatListModel> { };

        //    var currentUser = await _userManager.FindByEmailAsync("anhtrucphanit@gmail.com");
        //    var convOfCurrentUser = _parRepository.GetBy(x => x.UserId == currentUser.Id.ToString()).Include(inc => inc.ConversationEntity).Select(x => x.ConversationEntity).ToList();
        //    foreach (var item in convOfCurrentUser)
        //    {
        //        var contactOfCurrentUser = _parRepository.GetBy(x => x.UserId != currentUser.Id.ToString() && x.ConversationId == item.Id).FirstOrDefault();
        //        var convLastestMessage = _mesgRepository.GetBy(x => x.ConversationId == item.Id).OrderByDescending(x => x.Id).FirstOrDefault();
        //        var userContactInfo = _userInfoRepository.GetBy(x => x.UserId == contactOfCurrentUser.UserId).SingleOrDefault();
        //        contactList.Add(new ContactLatestChatListModel()
        //        {
        //            ConvId = item.Id,
        //            ContactId = contactOfCurrentUser.UserId,
        //            DisplayName = userContactInfo.DisplayName,
        //            LastMessage = convLastestMessage.Content,
        //            LastMessageTime = convLastestMessage.CreatedOn
        //        });
        //    }
        //    return contactList;
        //}


        public async Task<IEnumerable<IndexUserInfoModel>> GetUserUnknownContact(string username = null, string displayNameSearch = null)
        {
            var spName = "[dbo].[usp_Contact_GetUnknownContact]";
            var parameter = new[]
            {
                new SqlParameter
                {
                    ParameterName = "UserInfoId",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    SqlValue = string.IsNullOrEmpty(username) ? UserResolverService.CurrentUserInfoId() : username
                },
                new SqlParameter
                {
                    ParameterName = "DisplayNameSearch",
                    SqlDbType = SqlDbType.NVarChar,
                    SqlValue = displayNameSearch,
                    Size = 255
                }
            };

            var contactList =
                _userInfoContactRepository.ExecuteReturedStoredProcedure<IndexUserInfoModel>(spName, parameter);
            return await Task.FromResult(contactList);
        }

        public void DeleteContact(Guid id)
        {
            var spName = "[dbo].[usp_UserInfoContact_DeleteContact]";
            var parameter =
                new SqlParameter
                {
                    ParameterName = "Id",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    SqlValue = id
                };
            var contactList = _userInfoContactRepository.ExecuteReturedStoredProcedure<bool>(spName, parameter);
        }

        public void AddContact(AddContactModel addContactModel)
        {
            var spName = "[dbo].[usp_Contact_Add]";
            var parameter = new[]
            {
                new SqlParameter("UserId", SqlDbType.UniqueIdentifier)
                {
                    Value = string.IsNullOrEmpty(addContactModel.UserId)
                        ? UserResolverService.CurrentUserInfoId()
                        : addContactModel.UserId
                },
                new SqlParameter("ContactId", SqlDbType.UniqueIdentifier) {Value = addContactModel.ContactId},
                new SqlParameter("CreatedBy", SqlDbType.NVarChar, -1)
                {
                    Value = string.IsNullOrEmpty(UserResolverService.CurrentUserName())
                        ? "Root"
                        : UserResolverService.CurrentUserName()
                },
                new SqlParameter("NickName", SqlDbType.NVarChar, 120) {Value = addContactModel.NickName}
            };


            var contactList = _userInfoContactRepository.ExecuteReturedStoredProcedure<int>(spName, parameter);
        }

        public void EditContact(EditContactModel editContactModel)
        {
            var spName = "[dbo].[usp_UserInfoContact_EditContact]";
            var parameter =
                new[]
                {
                    new SqlParameter("Nickname", SqlDbType.NVarChar, 120) {Value = editContactModel.DisplayName},
                    new SqlParameter("Id", SqlDbType.UniqueIdentifier) {Value = editContactModel.Id}
                };
            var contactList = _userInfoContactRepository.ExecuteReturedStoredProcedure<bool>(spName, parameter);
        }

        public UserInfoContactEntity GetSingle(Guid id)
        {
            return _userInfoContactRepository.GetSingle(id);
        }

        public List<ContactLatestChatListModel> GetLatestContactChatListByUserId_SP()
        {
            var spName = "[dbo].[usp_Message_GetTheLatest]";
            var parameter =
                new SqlParameter
                {
                    ParameterName = "UserId",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    SqlValue = UserResolverService.CurrentUserId()
                };

            var contactList =
                _userInfoRepository.ExecuteReturedStoredProcedure<ContactLatestChatListModel>(spName, parameter);
            return contactList;
        }


        public async Task<List<ApplicationUser>> GetContacts(Guid conversationId)
        {
            var users = new List<ApplicationUser>();
            var participants = _parRepository.GetAll()
                .Where(x => x.ConversationId == conversationId && x.UserId != UserResolverService.CurrentUserId())
                .Select(x => x.UserId);
            foreach (var item in participants)
            {
                var user = await UserManager.FindByIdAsync(item);
                users.Add(user);
            }

            return users;
        }
    }
}