using GrooveMessengerDAL.Models;
using GrooveMessengerDAL.Repositories.Interface;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GrooveMessengerDAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext DbContext;
        private readonly IUserResolverService UserResolverService;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRepository(DbContext dbContext, IUserResolverService userResolverService, UserManager<ApplicationUser> userManager)
        {
            DbContext = dbContext;
            UserResolverService = userResolverService;
            _userManager = userManager;
        }
        public void Add(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public bool CheckExistingById(string id)
        {
            throw new NotImplementedException();
        }

        public void Delete(string entityId)
        {
            throw new NotImplementedException();
        }

        public void Edit(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ApplicationUser> FindBy(Expression<Func<ApplicationUser, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ApplicationUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> GetSingleAsync(string entityId)
        {
            var result = await _userManager.FindByEmailAsync(entityId);
            return result;
        }
    }
}
