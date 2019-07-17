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

            var storedData = _mesRepository.GetSingle(id);
            storedData.Deleted = true;
            _mesRepository.Edit(storedData);
            _uow.SaveChanges();
        }



        public void EditMessageModel(EditMessageModel data)
        {
            var storedData = _mesRepository.GetSingle(data.Id);
            storedData.Content = data.Content;

            _mesRepository.Edit(storedData);
            _uow.SaveChanges();
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
