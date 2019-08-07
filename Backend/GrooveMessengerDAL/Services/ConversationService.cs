﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GrooveMessengerDAL.Data;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models;
using GrooveMessengerDAL.Models.Conversation;
using GrooveMessengerDAL.Models.CustomModel;
using GrooveMessengerDAL.Models.PagingModel;
using GrooveMessengerDAL.Repositories.Interface;
using GrooveMessengerDAL.Services.Interface;
using GrooveMessengerDAL.Uow.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GrooveMessengerDAL.Services
{
    public class ConversationService : IConversationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMessageService _messageService;
        private readonly IUserResolverService _userResolverService;
        private readonly IGenericRepository<ConversationEntity, Guid, GrooveMessengerDbContext> _conRepository;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ParticipantEntity, Guid, GrooveMessengerDbContext> _parRepository;
        private readonly IUowBase<GrooveMessengerDbContext> _uow;

        public ConversationService(IGenericRepository<ConversationEntity, Guid, GrooveMessengerDbContext> conRepository,
            IGenericRepository<ParticipantEntity, Guid, GrooveMessengerDbContext> parRepository, IMapper mapper,
            IUowBase<GrooveMessengerDbContext> uow, IMessageService messageService,
            IUserResolverService userResolverService, UserManager<ApplicationUser> userManager)
        {
            _conRepository = conRepository;
            _parRepository = parRepository;
            _mapper = mapper;
            _uow = uow;
            _messageService = messageService;
            _userResolverService = userResolverService;
            _userManager = userManager;
        }

        public void AddConversation()
        {
         
            var conv = new ConversationEntity();
            conv.Avatar = "";
            conv.Name = "";
            _conRepository.Add(conv);
            _uow.SaveChanges();
        }

       

        public string GetGroupNameById(Guid id)
        {
            return _conRepository.GetBy(x => x.Id == id && x.IsGroup == true)
                .Select(s => s.Name)
                .SingleOrDefault();
        }

        public IEnumerable<ChatModel> GetAllConversationOfAUser(string userId)
        {
            var chatModels = new List<ChatModel>();
            var dialogDraftModels = GetAllConversationOfAUserDraft(userId);

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
                if (!chatModels.Select(x => x.Id).Contains(chatbox.Key.ToString())) {
                    List<DialogModel> dialogModels = new List<DialogModel>();
                    ChatModel chatModel = new ChatModel() { Id = chatbox.Key.ToString(), Dialog = dialogModels };
                    chatModels.Add(chatModel);
                    foreach (var message in chatbox.Dialogs)
                    {
                        var dialogModel = new DialogModel
                        { Message = message.Message, Who = message.Who, Time = message.Time };
                        dialogModels.Add(dialogModel);
                    }
                }
            }

            return chatModels;
        }
        public IEnumerable<DialogModel> GetAllConversationOfAUserDraft(string UserId = null)
        {
            var spName = "[dbo].[usp_Message_GetAllConversationsWithMessages]";
            var parameter =
                new SqlParameter
                {
                    ParameterName = "UserId",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    SqlValue = string.IsNullOrEmpty(UserId) ? _userResolverService.CurrentUserInfoId() : UserId
                };

            var contactList = _conRepository.ExecuteReturedStoredProcedure<DialogModel>(spName, parameter);
            return contactList;
        }
        
        public ChatModel GetConversationById(string conversationId, PagingParameterModel pagingParameterModel)
        {
            var spName = "[dbo].[usp_Message_GetByConversationId]";
            var parameter =
               new[]
               {
                    new SqlParameter("ConversationId", SqlDbType.UniqueIdentifier) {Value = conversationId},
                    new SqlParameter("CreatedOn", SqlDbType.DateTime2) {Value = pagingParameterModel.CreatedOn}
               };
        
            var contactList = _conRepository.ExecuteReturedStoredProcedure<DialogModel>(spName, parameter);

            var dialogModels = new List<DialogModel>();
            foreach (var item in contactList)
            {
                var dialogModel = new DialogModel { Id = item.Id, Who = item.Who, Message = item.Message, Time = item.Time, Avatar = item.Avatar, DisplayName = item.DisplayName };
                dialogModels.Add(dialogModel);
            }
            var chatModel = new ChatModel() { Id = conversationId, Dialog = dialogModels };
            return chatModel;
        }

        public void AddConversation(CreateConversationModel createMessageModel)
        {
            var mes = _mapper.Map<CreateConversationModel, ConversationEntity>(createMessageModel);
            _conRepository.Add(mes);
            _uow.SaveChanges();
        }

        public async Task<IEnumerable<IndexConversationModel>> GetGroupConversationsByUsername(string name)
        {
            var user = await _userManager.FindByEmailAsync(name);
            var groupConversation = _parRepository.GetBy(x => x.UserId == user.Id)
                .Include(ic => ic.ConversationEntity).Where(w => w.ConversationEntity.IsGroup == true).Select(s => s.ConversationEntity).ToList();
            return _mapper.Map<IEnumerable<ConversationEntity>, IEnumerable<IndexConversationModel>>(groupConversation);
        }

        public void editConversation(EditConversationModel editConversation)
        {
            var convEntity = _conRepository.GetSingle(editConversation.Id);
            convEntity.Avatar = editConversation.Avatar;
            convEntity.Name = editConversation.Name;
            _conRepository.Edit(convEntity);
            _uow.SaveChanges();
        }
        public IndexConversationModel getConversation(Guid Id)
        {
            var conv = _conRepository.GetBy(x => x.Id == Id).FirstOrDefault();
            return _mapper.Map<ConversationEntity, IndexConversationModel>(conv);
        }
    }
}