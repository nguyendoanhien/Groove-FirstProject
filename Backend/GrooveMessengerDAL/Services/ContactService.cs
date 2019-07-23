using AutoMapper;
using GrooveMessengerDAL.Data;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models;
using GrooveMessengerDAL.Models.Contact;
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
using System.Data;
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
        public async Task<IEnumerable<IndexUserInfoModel>> GetUserContactList(string username = null)
        {

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
            var spName = "[dbo].[usp_GetUserContactEmailList]";;
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

        public async Task<IEnumerable<IndexUserInfoModel>> GetUserUnknownContact(string username = null, string displayNameSearch = null)
        {
            var spName = "[dbo].[usp_Contact_GetUnknownContact]";
            var parameter = new SqlParameter[]
                {
                new SqlParameter
                {
                    ParameterName = "UserInfoId",
                    SqlDbType = System.Data.SqlDbType.UniqueIdentifier,
                    SqlValue = string.IsNullOrEmpty(username) ? _userResolverService.CurrentUserInfoId() : username
                },
                  new SqlParameter
                {
                    ParameterName = "DisplayNameSearch",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    SqlValue = displayNameSearch,
                    Size=255
                }
                };

            var contactList = _userInfoContactRepository.ExecuteReturedStoredProcedure<IndexUserInfoModel>(spName, parameter);
            return contactList;

        }

        public void DeleteContact(Guid Id)
        {
            var getContact = _userInfoContactRepository.GetSingle(Id);
            getContact.Deleted = true;
            _userInfoContactRepository.Edit(getContact);
            _uow.SaveChanges();

        }

        public void AddContact(AddContactModel addContactModel)
        {
            var spName = "[dbo].[usp_Contact_Add]";
            var parameter = new SqlParameter[]
            {
                  new SqlParameter("UserId",SqlDbType.UniqueIdentifier){Value=string.IsNullOrEmpty(addContactModel.UserId) ? _userResolverService.CurrentUserInfoId() : addContactModel.UserId},
                  new SqlParameter("ContactId",SqlDbType.UniqueIdentifier) {Value=addContactModel.ContactId},
                  new SqlParameter("CreatedBy",SqlDbType.NVarChar,-1) {Value=string.IsNullOrEmpty(_userResolverService.CurrentUserName()) ? "Root" : _userResolverService.CurrentUserName()},
                  new SqlParameter("NickName",SqlDbType.NVarChar,12) {Value=addContactModel.DisplayName},
            };


            var contactList = _userInfoContactRepository.ExecuteReturedStoredProcedure<int>(spName, parameter);

        }
        public void EditContact(EditContactModel editContactModel)
        {

            var getContact = _userInfoContactRepository.GetSingle(new Guid(editContactModel.Id));
            getContact.NickName = editContactModel.DisplayName;
            _userInfoContactRepository.Edit(getContact);
            _uow.SaveChanges();
        }

        public UserInfoContactEntity GetSingle(Guid Id)
        {
            return _userInfoContactRepository.GetSingle(Id);
        }

    }
}
