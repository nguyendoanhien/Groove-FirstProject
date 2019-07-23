﻿using AutoMapper;
using GrooveMessengerDAL.Data;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.CustomModel;
using GrooveMessengerDAL.Repositories.Interface;
using GrooveMessengerDAL.Services.Interface;
using GrooveMessengerDAL.Uow.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        private readonly IUserResolverService _userResolverService;
        private readonly IMessageService _messageService;

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
                
        public IEnumerable<ChatModel> GetAllConversationOfAUser(string UserId = null)
        {
            List<ChatModel> chatModels = new List<ChatModel>();
            IEnumerable<DialogDraftModel> dialogDraftModels = GetAllConversationOfAUserDraft(UserId);

            var chatBoxes = from chat in dialogDraftModels
                            group chat by chat.Id into chatGroup
                            select new
                            {
                                Key = chatGroup.Key,
                                Dialogs = chatGroup
                            };
            foreach (var chatbox in chatBoxes)
            {
                List<DialogModel> dialogModels = new List<DialogModel>();
                ChatModel chatModel = new ChatModel() { Id = chatbox.Key, Dialog = dialogModels };
                chatModels.Add(chatModel);
                foreach (var message in chatbox.Dialogs)
                {
                    DialogModel dialogModel = new DialogModel() { Message = message.Message, Who = message.Who, Time = message.Time };
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
                    SqlDbType = System.Data.SqlDbType.UniqueIdentifier,
                    SqlValue = string.IsNullOrEmpty(UserId) ? _userResolverService.CurrentUserInfoId() : UserId
                };

            var contactList = _conRepository.ExecuteReturedStoredProcedure<DialogDraftModel>(spName, parameter);
            return contactList;
        }
    }
}
