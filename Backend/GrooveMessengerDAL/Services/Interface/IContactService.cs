﻿using GrooveMessengerDAL.Models.Contact;
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
        Task<IEnumerable<IndexUserInfoModel>> GetUserContact(string username = null);
        Task<IEnumerable<IndexUserInfoModel>> GetUserUnknownContact(string username = null);
        void DeleteContact(DeleteContactModel deleteContactModel);
        void AddContact(AddContactModel addContactModel);
        void EditContact(EditContactModel addContactModel);
    }
}
