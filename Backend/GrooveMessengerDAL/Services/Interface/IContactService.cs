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
        Task DeleteContact(string contactId, string username = null);
        Task AddContact(string contactId, string username = null);
    }
}
