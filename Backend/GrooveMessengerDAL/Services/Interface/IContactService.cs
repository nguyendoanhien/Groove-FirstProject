using GrooveMessengerDAL.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrooveMessengerDAL.Services.Interface
{
    public interface IContactService
    {
        Task<IEnumerable<IndexUserInfoModel>> GetAllContact(string username = null);
    }
}
