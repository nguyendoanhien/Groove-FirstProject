using AutoMapper;
using GrooveMessengerDAL.Data;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.Message;
using GrooveMessengerDAL.Repositories.Interface;
using GrooveMessengerDAL.Services.Interface;
using GrooveMessengerDAL.Uow.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GrooveMessengerDAL.Services
{
    public class MessageService : IMessageService
    {
        private IGenericRepository<MessageEntity, Guid, GrooveMessengerDbContext> _noteRepository;
        private IMapper _mapper;
        private IUowBase<GrooveMessengerDbContext> _uow;



        public MessageService(
            IGenericRepository<MessageEntity, Guid, GrooveMessengerDbContext> notesRepo,
            IUowBase<GrooveMessengerDbContext> uow,
            IMapper mapper
            )
        {
            this._noteRepository = notesRepo;
            this._uow = uow;
            this._mapper = mapper;
        }

        public bool CheckExisting(Guid id)
        {
            var result = _noteRepository.CheckExistingById(id);
            return result;
        }

        public void DeleteMessage(Guid id)
        {

            var storedData = _noteRepository.GetSingle(id);
            storedData.Deleted = true;
            _noteRepository.Edit(storedData);
            _uow.SaveChanges();
        }

      

        public void EditMessageModel(EditMessageModel data)
        {
            var storedData = _noteRepository.GetSingle(data.Id);
            storedData.Content= data.Content;
     
            _noteRepository.Edit(storedData);
            _uow.SaveChanges();
        }

        public EditMessageModel GetMessageForEdit(Guid id)
        {
            var storedData = _noteRepository.GetSingle(id);
            var result = _mapper.Map<MessageEntity, EditMessageModel>(storedData);
            return result;
        }

        public async Task<EditMessageModel> GetMessageForEditAsync(Guid id)
        {
            //throw new NotImplementedException();
            var storedData = await _noteRepository.GetSingleAsync(id);
            var result = _mapper.Map<MessageEntity,EditMessageModel>(storedData);
            return result;
        }

        
    }
}
