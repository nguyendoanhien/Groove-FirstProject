using AutoMapper;
using GrooveMessengerDAL.Data;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.Message;
using GrooveMessengerDAL.Repositories.Interface;
using GrooveMessengerDAL.Services.Interface;
using GrooveMessengerDAL.Uow.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public MessageEntity GetMessageById(Guid Id)
        {
            var messages = _mesRepository.GetSingle(Id);
            return messages;
        }


        public void UpdateStatusMessage(Guid Id)
        {
            var message = _mesRepository.GetSingle(Id);
            message.SeenBy = "Seen";
            _mesRepository.Edit(message);
            _uow.SaveChanges();
        }
    }
}
