using GrooveMessengerDAL.Models.CustomModel;
using GrooveMessengerDAL.Models.User;
﻿using GrooveMessengerDAL.Models.Contact;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrooveMessengerDAL.Services.Interface
{
    public interface IContactService
    {        
        Task<List<ContactLatestChatListModel>> GetLatestContactChatListByUserId();
        Task<IEnumerable<IndexUserInfoModel>> GetUserContactList(string username = null);
        Task<IEnumerable<string>> GetUserContactEmailList(string username = null);
        Task<IEnumerable<IndexUserInfoModel>> GetUserUnknownContact(string username = null);
        void DeleteContact(DeleteContactModel deleteContactModel);
        void AddContact(AddContactModel addContactModel);
        void EditContact(EditContactModel addContactModel);
    }
}
