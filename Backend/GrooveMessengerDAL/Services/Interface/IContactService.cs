using GrooveMessengerDAL.Models.CustomModel;
using GrooveMessengerDAL.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrooveMessengerDAL.Services.Interface
{
    public interface IContactService
    {
        Task<IEnumerable<IndexUserInfoModel>> GetAllContact(string username = null);
        Task<List<ContactLatestChatListModel>> GetLatestContactChatListByUserId();
        //IQueryable<FullContactModel> GetFromUsername(string userName);
        //IQueryable<FullContactModel> GetContacts();
    }
}
