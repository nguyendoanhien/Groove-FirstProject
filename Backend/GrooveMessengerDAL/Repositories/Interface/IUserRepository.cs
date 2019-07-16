using GrooveMessengerDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GrooveMessengerDAL.Repositories.Interface
{
    public interface IUserRepository
   {
        IQueryable<ApplicationUser> GetAll();
        Task<ApplicationUser> GetSingleAsync(string entityId);
        IQueryable<ApplicationUser> FindBy(Expression<Func<ApplicationUser, bool>> predicate);
        void Add(ApplicationUser entity);
        void Delete(string entityId);
        void Edit(ApplicationUser entity);
        bool CheckExistingById(string id);
    }
}
