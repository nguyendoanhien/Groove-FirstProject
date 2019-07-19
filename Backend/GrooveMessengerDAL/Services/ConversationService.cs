using AutoMapper;
using GrooveMessengerDAL.Data;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.CustomModel;
using GrooveMessengerDAL.Repositories.Interface;
using GrooveMessengerDAL.Services.Interface;
using GrooveMessengerDAL.Uow.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GrooveMessengerDAL.Services
{
    public class ConversationService : IConversationService
    {
        private IGenericRepository<ConversationEntity, Guid, GrooveMessengerDbContext> _conRepository;
        private IGenericRepository<ParticipantEntity, Guid, GrooveMessengerDbContext> _parRepository;
        private IMapper _mapper;
        private IUowBase<GrooveMessengerDbContext> _uow;
        private IParticipantService _participantService;

        private readonly IMessageService _messageService;

        public ConversationService(IGenericRepository<ConversationEntity, Guid, GrooveMessengerDbContext> conRepository,
            IGenericRepository<ParticipantEntity, Guid, GrooveMessengerDbContext> parRepository, IMapper mapper, 
            IUowBase<GrooveMessengerDbContext> uow, IMessageService messageService)   
        {
            _conRepository = conRepository;
            _parRepository = parRepository;
            _mapper = mapper;
            _uow = uow;
            _messageService = messageService;
        }

        public void AddConversation()
        {
            ConversationEntity conv = new ConversationEntity();
            conv.Avatar = "";
            conv.Name = "";
            _conRepository.Add(conv);
            _uow.SaveChanges();
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


        public IEnumerable<ChatModel> GetAllConversationOfAUser(string UserId)
        {
            var conversationList = _parRepository.GetAll().Where(x => x.UserId == UserId).Select(x => x.ConversationEntity).ToList();
            List<ChatModel> chats = new List<ChatModel>();
            foreach (ConversationEntity item in conversationList)
            {
                var dialogs = _messageService.GetDialogs(item.Id);
                ChatModel chatModel = new ChatModel()
                {
                    Id = item.Id.ToString(),
                    Dialog = dialogs.ToList()
                };
                chats.Add(chatModel);
            }
            return chats;
        }
        public ChatModel GetConversationOfAUser(string ConversationId)
        {
            var dialogs = _messageService.GetDialogs(Guid.Parse(ConversationId));
            ChatModel chatModel = new ChatModel()
            {
                Id = ConversationId,
                Dialog = dialogs.ToList()
            };
            return chatModel;
        }
    }
}
