using AutoMapper;
using GrooveMessengerDAL.Data;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.Message;
using GrooveMessengerDAL.Repositories.Interface;
using GrooveMessengerDAL.Services.Interface;
using GrooveMessengerDAL.Uow.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using GrooveMessengerDAL.Models.CustomModel;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data.SqlClient;

namespace GrooveMessengerDAL.Services
{
    public class MessageService : IMessageService
    {
        private IGenericRepository<MessageEntity, Guid, GrooveMessengerDbContext> _mesRepository;
        private IMapper _mapper;
        private IUowBase<GrooveMessengerDbContext> _uow;
       
        public MessageService(IGenericRepository<MessageEntity, Guid, GrooveMessengerDbContext> mesRepository, IMapper mapper, IUowBase<GrooveMessengerDbContext> uow)
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
                SqlDbType = System.Data.SqlDbType.UniqueIdentifier,
                SqlValue = id,
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
            var parameter = new SqlParameter[]
            {
                new SqlParameter("Content",System.Data.SqlDbType.NVarChar,1000){Value = data.Content},
                new SqlParameter("Id",System.Data.SqlDbType.UniqueIdentifier){Value = data.Id}
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

        public IEnumerable<MessageEntity> loadMoreMessages(int pageNumber, int pageSize)
        {
            var messages = _mesRepository.GetAll().OrderByDescending(x=>x.CreatedOn);
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

        public MessageEntity GetMessageById(Guid Id)
        {
            var messages = _mesRepository.GetSingle(Id);
            return messages;
        }


        public int SetValueSeenBy(string userId, Guid conversationId)
        {
            var spName = "[dbo].[msp_SetValueSeenBy]";
            var parameter1 =
                new SqlParameter
                {
                    ParameterName = "conversationId",
                    SqlDbType = System.Data.SqlDbType.UniqueIdentifier,
                    SqlValue = conversationId
                };
            var parameter2 =
                new SqlParameter
                {
                    ParameterName = "userId",
                    SqlDbType = System.Data.SqlDbType.UniqueIdentifier,
                    SqlValue = userId
                };

            return _mesRepository.ExecuteReturedStoredProcedure<int>(spName, parameter2, parameter1).FirstOrDefault();
        }
        public IEnumerable<DialogModel> GetDialogs(Guid ConversationId)
        {
            var messageList = _mesRepository.GetAll().Where(x => x.ConversationId == ConversationId)
                .OrderBy(x=>x.CreatedOn).ToList();
            List<DialogModel> dialogs = new List<DialogModel>();
            foreach (MessageEntity item in messageList)
            {
                DialogModel dialog = new DialogModel()
                {
                    Who = item.SenderId,
                    Message = item.Content,
                    Time = item.CreatedOn
                };
                dialogs.Add(dialog);
            }
            return dialogs;
        }
        public void GetAllMsg()
        {
            var msgs = _mesRepository.GetAll();
            _uow.SaveChanges();
        }
        public int GetUnreadMessages(Guid conversationId, string userId)
        {
            var spName = "[dbo].[msp_Message_GetUnreadMessage]";
            var parameter1 =
                new SqlParameter
                {
                    ParameterName = "conversationId",
                    SqlDbType = System.Data.SqlDbType.UniqueIdentifier,
                    SqlValue = conversationId
                };
            var parameter2 =
                new SqlParameter
                {
                    ParameterName = "userId",
                    SqlDbType = System.Data.SqlDbType.UniqueIdentifier,
                    SqlValue = userId
                };

            return _mesRepository.ExecuteReturedStoredProcedure<int>(spName, parameter2, parameter1).FirstOrDefault();

        }
    }
}
