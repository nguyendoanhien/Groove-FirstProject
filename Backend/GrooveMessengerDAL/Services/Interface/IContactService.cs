using GrooveMessengerDAL.Models.CustomModel;
using GrooveMessengerDAL.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrooveMessengerDAL.Services.Interface
{
    public interface IContactService
    {        
        Task<List<ContactLatestChatListModel>> GetLatestContactChatListByUserId();
        Task<IEnumerable<IndexUserInfoModel>> GetUserContact(string username = null);
        Task<IEnumerable<IndexUserInfoModel>> GetUserUnknownContact(string username = null);
        Task DeleteContact(string contactId, string username = null);
        Task AddContact(string contactId, string username = null);
    }
}
