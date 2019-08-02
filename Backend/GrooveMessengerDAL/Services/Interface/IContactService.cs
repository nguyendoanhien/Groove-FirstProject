using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models;
using GrooveMessengerDAL.Models.Contact;
using GrooveMessengerDAL.Models.CustomModel;
using GrooveMessengerDAL.Models.User;

namespace GrooveMessengerDAL.Services.Interface
{
    public interface IContactService
    {
        //Task<List<ContactLatestChatListModel>> GetLatestContactChatListByUserId();
        List<ContactLatestChatListModel> GetLatestContactChatListByUserId_SP();
        Task<IEnumerable<IndexUserInfoModel>> GetUserContactList(string username = null);
        Task<IEnumerable<string>> GetUserContactEmailList(string username = null);
        Task<string> GetUserContactEmail(string userId);

        Task<IEnumerable<IndexUserInfoModel>> GetUserUnknownContact(string username = null,
            string displayNameSearch = null);

        void DeleteContact(Guid id);
        void AddContact(AddContactModel addContactModel);
        void EditContact(EditContactModel addContactModel);
        UserInfoContactEntity GetSingle(Guid id);
        List<string> GetContacts(Guid conversationId, Guid userId);
    }
}