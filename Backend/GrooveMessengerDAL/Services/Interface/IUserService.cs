using GrooveMessengerDAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GrooveMessengerDAL.Services.Interface
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUser(string id);
    }
}
