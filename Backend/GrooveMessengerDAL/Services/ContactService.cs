using AutoMapper;
using GrooveMessengerDAL.Data;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models;
using GrooveMessengerDAL.Models.Contact;
using GrooveMessengerDAL.Repositories.Interface;
using GrooveMessengerDAL.Services.Interface;
using GrooveMessengerDAL.Uow.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GrooveMessengerDAL.Services
{
    public class ContactService : IContactService
    {
        private IGenericRepository<UserInfoContactEntity, Guid, GrooveMessengerDbContext> _userContactRepository;
        private IMapper _mapper;
        private IUowBase<GrooveMessengerDbContext> _uow;
        private readonly UserManager<ApplicationUser> _userManager;
        public ContactService(
            IGenericRepository<UserInfoContactEntity, Guid, GrooveMessengerDbContext> userRepository,
            IMapper mapper,
            IUowBase<GrooveMessengerDbContext> uow,
            UserManager<ApplicationUser> userManager
            )
        {
            _userContactRepository = userRepository;
            _mapper = mapper;
            _uow = uow;
            _userManager = userManager;
        }
        public IQueryable<FullContactModel> GetFromUsername(string userName)
        {

            var res = _userContactRepository.GetBy((m) => m.UserInfo.ApplicationUser.UserName == userName);
            var reRes = _mapper.Map<IQueryable<UserInfoContactEntity>, IQueryable<FullContactModel>>(res);
            return reRes;
        }
        public IQueryable<FullContactModel> GetContacts()
        {
            var res = _userContactRepository.GetAll();
            var reRes = _mapper.Map<IQueryable<UserInfoContactEntity>, IQueryable<FullContactModel>>(res);
            return reRes;
        }

    }
}
