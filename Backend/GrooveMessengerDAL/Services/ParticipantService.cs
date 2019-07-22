using AutoMapper;
using GrooveMessengerDAL.Data;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.Participant;
using GrooveMessengerDAL.Repositories.Interface;
using GrooveMessengerDAL.Services.Interface;
using GrooveMessengerDAL.Uow.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GrooveMessengerDAL.Services
{
    public class ParticipantService : IParticipantService
    {
        private IGenericRepository<ParticipantEntity, Guid, GrooveMessengerDbContext> _parRepository;
        private IMapper _mapper;
        private IUowBase<GrooveMessengerDbContext> _uow;

        public ParticipantService(IGenericRepository<ParticipantEntity, Guid, GrooveMessengerDbContext> parRepository, IMapper mapper, IUowBase<GrooveMessengerDbContext> uow)
        {
            _parRepository = parRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public IEnumerable<Guid> GetAllConversationIdOfAUser(string UserId)
        {
            return _parRepository.GetBy(x => x.UserId == UserId).Select(x=>x.ConversationId).Distinct().ToList();
        }

        public void NewParticipant(ParticipantModel participantModel)
        {
            var par = _mapper.Map<ParticipantModel, ParticipantEntity>(participantModel);
            _parRepository.Add(par);
            _uow.SaveChanges();
        }
    }
}
