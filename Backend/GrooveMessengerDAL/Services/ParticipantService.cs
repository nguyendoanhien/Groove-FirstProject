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

        public ParticipantService(IGenericRepository<ParticipantEntity, Guid, GrooveMessengerDbContext> parRepository,
            IMapper mapper, IUowBase<GrooveMessengerDbContext> uow)
        {
            _parRepository = parRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public IEnumerable<Guid> GetAllConversationIdOfAUser(string UserId)
        {
            return _parRepository.GetBy(x => x.UserId == UserId).Select(x => x.ConversationId).Distinct().ToList();
        }

        public void AddParticipant(ParticipantModel participantModel)
        {
            var par = _mapper.Map<ParticipantModel, ParticipantEntity>(participantModel);
            _parRepository.Add(par);
            _uow.SaveChanges();
        }
    }
}