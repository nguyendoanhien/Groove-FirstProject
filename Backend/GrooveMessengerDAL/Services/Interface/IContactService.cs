using GrooveMessengerDAL.Models.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrooveMessengerDAL.Services.Interface
{
    public interface IContactService
    {

        IQueryable<FullContactModel> GetFromUsername(string userName);
        IQueryable<FullContactModel> GetContacts();
    }
}
