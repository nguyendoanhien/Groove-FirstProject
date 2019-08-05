using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GrooveMessengerDAL.Data;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.Participant;
using GrooveMessengerDAL.Repositories.Interface;
using GrooveMessengerDAL.Services.Interface;
using GrooveMessengerDAL.Uow.Interface;

namespace GrooveMessengerDAL.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ParticipantEntity, Guid, GrooveMessengerDbContext> _parRepository;
        private readonly IUowBase<GrooveMessengerDbContext> _uow;
        private readonly IUserResolverService _userResolver;

        public ParticipantService(IGenericRepository<ParticipantEntity, Guid, GrooveMessengerDbContext> parRepository,
            IMapper mapper, IUowBase<GrooveMessengerDbContext> uow, IUserResolverService userResolver)
        {
            _parRepository = parRepository;
            _mapper = mapper;
            _uow = uow;
            _userResolver = userResolver;
        }

        public IEnumerable<Guid> GetAllConversationIdOfAUser(string userId)
        {
            return _parRepository.GetBy(x => x.UserId == userId).Select(x => x.ConversationId).Distinct().ToList();
        }

        public void AddParticipant(ParticipantModel participantModel)
        {
            var par = _mapper.Map<ParticipantModel, ParticipantEntity>(participantModel);
            _parRepository.Add(par);
            _uow.SaveChanges();
        }

        public IEnumerable<string> GetParticipantUsersByConversation(Guid id)
        {
            return _parRepository.GetBy(x => x.ConversationId == id && x.UserId != _userResolver.CurrentUserId().ToString()).Select(s => s.UserId).ToList();
        }
    }
}