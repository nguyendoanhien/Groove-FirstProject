using AutoMapper;
using GrooveMessengerDAL.Data;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.Conversation;
using GrooveMessengerDAL.Repositories.Interface;
using GrooveMessengerDAL.Services.Interface;
using GrooveMessengerDAL.Uow.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrooveMessengerDAL.Services
{
    public class ConversationService: IConversationService
    {
        private IGenericRepository<ConversationEntity, Guid, GrooveMessengerDbContext> _conRepository;
        private IGenericRepository<MessageEntity, Guid, GrooveMessengerDbContext> _mesRepository;
        private IMapper _mapper;
        private IUowBase<GrooveMessengerDbContext> _uow;
        private IParticipantService _participantService;
        public ConversationService(IGenericRepository<ConversationEntity, Guid, GrooveMessengerDbContext> conRepository, IMapper mapper, IUowBase<GrooveMessengerDbContext> uow, IParticipantService participantService)
        {
            _participantService = participantService;
            _conRepository = conRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public void AddConversation()
        {
            //TODO: do business here
            ConversationEntity conv = new ConversationEntity();
            conv.Avatar = "";
            conv.Name = "";
            _conRepository.Add(conv);
            _uow.SaveChanges();
        }



        public IEnumerable<ConversationEntity> GetConversations(string UserId)
        {
            List<Guid> conIdList = _participantService.GetAllConversationIdOfAUser(UserId).ToList();
            var result = _conRepository.GetAll().Where(x => conIdList.Contains(x.Id));
            return result;
        }

        //public IndexConversationModel getGetConversationById(Guid id)
        //{
        //    var entity = _conRepository.FindBy(x => x.Id == id).Include(i => i.MessageEntity).FirstOrDefault();
        //    var result = _mapper.Map<ConversationEntity, IndexConversationModel>(entity);
        //    return result;
        //}

        //public IEnumerable<ConversationEntity> GetConversations(string UserId)
        //{
        //    List<Guid> conIdList = _participantService.GetAllConversationIdOfAUser(UserId).ToList();
        //    var result = _conRepository.GetAll().Where(x => conIdList.Contains(x.Id));
        //    return result;
        //}



    }
}
