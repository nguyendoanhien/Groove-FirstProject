using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GrooveMessengerDAL.Data;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.CustomModel;
using GrooveMessengerDAL.Models.Message;
using GrooveMessengerDAL.Repositories.Interface;
using GrooveMessengerDAL.Services.Interface;
using GrooveMessengerDAL.Uow.Interface;

namespace GrooveMessengerDAL.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<MessageEntity, Guid, GrooveMessengerDbContext> _mesRepository;
        private readonly IUowBase<GrooveMessengerDbContext> _uow;

        public MessageService(IGenericRepository<MessageEntity, Guid, GrooveMessengerDbContext> mesRepository,
            IMapper mapper, IUowBase<GrooveMessengerDbContext> uow)
        {
            _mesRepository = mesRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public bool CheckExisting(Guid id)
        {
            var result = _mesRepository.CheckExistingById(id);
            return result;
        }

        public void DeleteMessage(Guid id)
        {
            var spName = "[dbo].[usp_Message_DeleteMessage]";
            var parameter = new SqlParameter
            {
                ParameterName = "Id",
                SqlDbType = SqlDbType.UniqueIdentifier,
                SqlValue = id
            };

            var delMsg = _mesRepository.ExecuteReturedStoredProcedure<bool>(spName, parameter);


            //var storedData = _mesRepository.GetSingle(id);
            //storedData.Deleted = true;
            //_mesRepository.Edit(storedData);
            //_uow.SaveChanges();
        }

        public void EditMessageModel(EditMessageModel data)
        {
            var spName = "[dbo].[usp_Message_EditMessage]";
            var parameter = new[]
            {
                new SqlParameter("Content", SqlDbType.NVarChar, 1000) {Value = data.Content},
                new SqlParameter("Id", SqlDbType.UniqueIdentifier) {Value = data.Id}
            };

            var editMsg = _mesRepository.ExecuteReturedStoredProcedure<bool>(spName, parameter);
            //var storedData = _mesRepository.GetSingle(data.Id);
            //storedData.Content = data.Content;
            //_mesRepository.Edit(storedData);
            //_uow.SaveChanges();
        }

        public EditMessageModel GetMessageForEdit(Guid id)
        {
            var storedData = _mesRepository.GetSingle(id);
            var result = _mapper.Map<MessageEntity, EditMessageModel>(storedData);
            return result;
        }

        public async Task<EditMessageModel> GetMessageForEditAsync(Guid id)
        {
            var storedData = await _mesRepository.GetSingleAsync(id);
            var result = _mapper.Map<MessageEntity, EditMessageModel>(storedData);
            return result;
        }

        public IEnumerable<MessageEntity> LoadMoreMessages(int pageNumber, int pageSize)
        {
            var messages = _mesRepository.GetAll().OrderByDescending(x => x.CreatedOn);
            var result = messages.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return result;
        }

        public void AddMessage(CreateMessageModel msg)
        {
            var mes = _mapper.Map<CreateMessageModel, MessageEntity>(msg);
            _mesRepository.Add(mes);
            _uow.SaveChanges();
        }

        public async Task<IndexMessageModel> AddMessageAsync(CreateMessageModel msg)
        {
            var mes = _mapper.Map<CreateMessageModel, MessageEntity>(msg);
            var addedMessage = _mesRepository.Add(mes);
            await _uow.SaveChangesAsync();
            return _mapper.Map<MessageEntity, IndexMessageModel>(addedMessage.Entity);
        }

        public MessageEntity GetMessageById(Guid id)
        {
            var messages = _mesRepository.GetSingle(id);
            return messages;
        }


        public int SetValueSeenBy(string userId, Guid conversationId)
        {
            var spName = "[dbo].[usp_Message_SetValueSeenBy]";
            var parameter1 =
                new SqlParameter
                {
                    ParameterName = "conversationId",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    SqlValue = conversationId
                };
            var parameter2 =
                new SqlParameter
                {
                    ParameterName = "userId",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    SqlValue = userId
                };

            return _mesRepository.ExecuteReturedStoredProcedure<int>(spName, parameter2, parameter1).FirstOrDefault();
        }

        public IEnumerable<DialogModel> GetDialogs(Guid conversationId)
        {
            var messageList = _mesRepository.GetAll().Where(x => x.ConversationId == conversationId)
                .OrderByDescending(x => x.CreatedOn).ToList();
            var dialogs = new List<DialogModel>();
            foreach (var item in messageList)
            {
                var dialog = new DialogModel
                {
                    Who = item.SenderId,
                    Message = item.Content,
                    Time = item.CreatedOn
                };
                dialogs.Add(dialog);
            }

            return dialogs;
        }

        public int GetUnreadMessages(Guid conversationId, string userName)
        {
            var spName = "[dbo].[usp_Message_GetUnreadMessageAmount]";
            var parameter1 =
                new SqlParameter
                {
                    ParameterName = "conversationId",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    SqlValue = conversationId
                };
            var parameter2 =
                new SqlParameter
                {
                    ParameterName = "userName",
                    SqlDbType = SqlDbType.NVarChar,
                    SqlValue = userName,
                    Size = 256
                };
            return _mesRepository.ExecuteReturedStoredProcedure<int>(spName, parameter2, parameter1).FirstOrDefault();
        }

        public void GetAllMsg()
        {
            var msgs = _mesRepository.GetAll();
            _uow.SaveChanges();
        }
    }
}