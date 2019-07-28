using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using AutoMapper;
using GrooveMessengerDAL.Data;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.Conversation;
using GrooveMessengerDAL.Models.CustomModel;
using GrooveMessengerDAL.Repositories.Interface;
using GrooveMessengerDAL.Services.Interface;
using GrooveMessengerDAL.Uow.Interface;

namespace GrooveMessengerDAL.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IMessageService _messageService;
        private readonly IUserResolverService _userResolverService;
        private readonly IGenericRepository<ConversationEntity, Guid, GrooveMessengerDbContext> _conRepository;
        private readonly IMapper _mapper;
        private IGenericRepository<MessageEntity, Guid, GrooveMessengerDbContext> _mesRepository;
        private IGenericRepository<ParticipantEntity, Guid, GrooveMessengerDbContext> _parRepository;
        private IParticipantService _participantService;
        private readonly IUowBase<GrooveMessengerDbContext> _uow;

        public ConversationService(IGenericRepository<ConversationEntity, Guid, GrooveMessengerDbContext> conRepository,
            IGenericRepository<ParticipantEntity, Guid, GrooveMessengerDbContext> parRepository, IMapper mapper,
            IUowBase<GrooveMessengerDbContext> uow, IMessageService messageService,
            IUserResolverService userResolverService)
        {
            _conRepository = conRepository;
            _parRepository = parRepository;
            _mapper = mapper;
            _uow = uow;
            _messageService = messageService;
            _userResolverService = userResolverService;
        }

        public void AddConversation()
        {
            //TODO: do business here
            var conv = new ConversationEntity();
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

        public IEnumerable<ChatModel> GetAllConversationOfAUser(string UserId = null)
        {
            var chatModels = new List<ChatModel>();
            var dialogDraftModels = GetAllConversationOfAUserDraft(UserId);

            var chatBoxes = from chat in dialogDraftModels
                group chat by chat.Id
                into chatGroup
                select new
                {
                    chatGroup.Key,
                    Dialogs = chatGroup
                };
            foreach (var chatbox in chatBoxes)
            {
                var dialogModels = new List<DialogModel>();
                var chatModel = new ChatModel {Id = chatbox.Key, Dialog = dialogModels};
                chatModels.Add(chatModel);
                foreach (var message in chatbox.Dialogs)
                {
                    var dialogModel = new DialogModel
                        {Message = message.Message, Who = message.Who, Time = message.Time};
                    dialogModels.Add(dialogModel);
                }
            }

            return chatModels;
        }

        public IEnumerable<DialogDraftModel> GetAllConversationOfAUserDraft(string UserId = null)
        {
            var spName = "[dbo].[msp_GetAllConversationsWithMessages]";
            var parameter =
                new SqlParameter
                {
                    ParameterName = "UserId",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    SqlValue = string.IsNullOrEmpty(UserId) ? _userResolverService.CurrentUserInfoId() : UserId
                };

            var contactList = _conRepository.ExecuteReturedStoredProcedure<DialogDraftModel>(spName, parameter);
            return contactList;
        }

        public ChatModel GetConversationById(string ConversationId)
        {
            var spName = "[dbo].[csp_GetConversationById]";
            var parameter =
                new SqlParameter
                {
                    ParameterName = "ConversationId",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    SqlValue = string.IsNullOrEmpty(ConversationId)
                        ? _userResolverService.CurrentUserInfoId()
                        : ConversationId
                };

            var contactList = _conRepository.ExecuteReturedStoredProcedure<DialogDraftModel>(spName, parameter);

            var dialogModels = new List<DialogModel>();
            foreach (var item in contactList)
            {
                var dialogModel = new DialogModel {Who = item.Who, Message = item.Message, Time = item.Time};
                dialogModels.Add(dialogModel);
            }

            var chatModel = new ChatModel {Id = Guid.Parse(ConversationId), Dialog = dialogModels};
            return chatModel;
        }

        public void AddConversation(CreateConversationModel createMessageModel)
        {
            var mes = _mapper.Map<CreateConversationModel, ConversationEntity>(createMessageModel);
            _conRepository.Add(mes);
            _uow.SaveChanges();
        }

        public ChatModel GetConversationOfAUser(string ConversationId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ConversationEntity> GetConversations(string UserId)
        {
            var conIdList = _participantService.GetAllConversationIdOfAUser(UserId).ToList();
            var result = _conRepository.GetAll().Where(x => conIdList.Contains(x.Id));
            return result;
        }
    }
}