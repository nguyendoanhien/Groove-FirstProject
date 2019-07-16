using AutoMapper;
using GrooveMessengerDAL.Data;
using GrooveMessengerDAL.Models;
using GrooveMessengerDAL.Models.User;
using GrooveMessengerDAL.Repositories.Interface;
using GrooveMessengerDAL.Services.Interface;
using GrooveMessengerDAL.Uow.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GrooveMessengerDAL.Services
{
   public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private IUowBase<GrooveMessengerDbContext> _uow;

        public UserService(
            IUserRepository userRepository,
            IUowBase<GrooveMessengerDbContext> uow,
            IMapper mapper
            )
        {
            this._userRepository = userRepository;
            this._uow = uow;
            this._mapper = mapper;
        }
        public async Task<ApplicationUser> GetUser(string id)
        {
            return  await _userRepository.GetSingleAsync(id);

        }
    }
}
