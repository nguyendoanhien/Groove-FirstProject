using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.Contact;
using GrooveMessengerDAL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrooveMessengerDAL.Services.Interface
{
    public interface IContactService
    {
        Task<IEnumerable<IndexUserInfoModel>> GetUserContactList(string username = null);
        Task<IEnumerable<string>> GetUserContactEmailList(string username = null);
        Task<IEnumerable<IndexUserInfoModel>> GetUserUnknownContact(string username = null, string displayNameSearch = null);
        void DeleteContact(Guid Id);
        void AddContact(AddContactModel addContactModel);
        void EditContact(EditContactModel addContactModel);
        UserInfoContactEntity GetSingle(Guid Id);
    }
}
