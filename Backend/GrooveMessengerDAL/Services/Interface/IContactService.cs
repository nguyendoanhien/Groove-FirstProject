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
        Task<IEnumerable<IndexUserInfoModel>> GetAllContact(string username = null);
        void Add(FullContactModel contact);
        //IQueryable<FullContactModel> GetFromUsername(string userName);
        //IQueryable<FullContactModel> GetContacts();
    }
}
