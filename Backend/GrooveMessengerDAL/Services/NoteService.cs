using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using AutoMapper;
using GrooveMessengerDAL.Data;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.Note;
using GrooveMessengerDAL.Repositories.Interface;
using GrooveMessengerDAL.Services.Interface;
using GrooveMessengerDAL.Uow.Interface;

namespace GrooveMessengerDAL.Services
{
    public class NoteService : INoteService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<NoteEntity, int, GrooveMessengerDbContext> _noteRepository;
        private readonly IUowBase<GrooveMessengerDbContext> _uow;

        public NoteService(
            IGenericRepository<NoteEntity, int, GrooveMessengerDbContext> notesRepo,
            IUowBase<GrooveMessengerDbContext> uow,
            IMapper mapper
        )
        {
            _noteRepository = notesRepo;
            _uow = uow;
            _mapper = mapper;
        }

        public IEnumerable<IndexModel> GetNoteList()
        {
            var storedData = _noteRepository.GetAll();
            var result = _mapper.Map<IEnumerable<NoteEntity>, IEnumerable<IndexModel>>(storedData);
            return result;
        }

        //public IEnumerable<FullModel> GetNoteListFullModel()
        //{
        //    var storedData = _noteRepository.GetAll();
        //    var result = _mapper.Map<IEnumerable<NoteEntity>, IEnumerable<FullModel>>(storedData);
        //    return result;
        //}

        public IEnumerable<FullModel> GetNoteListFullModel()
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(
                new SqlParameter
                {
                    ParameterName = "title",
                    SqlDbType = SqlDbType.NVarChar,
                    SqlValue = "This is a test ';--"
                });
            parameters.Add(
                new SqlParameter
                {
                    ParameterName = "createdOn",
                    SqlDbType = SqlDbType.DateTime2,
                    SqlValue = "2019-02-03 00:00:00"
                });
            //var storedData = _noteRepository.ExecuteReturedStoredProcedure("usp_Notes_GetData", parameters.ToArray());
            //var result = _mapper.Map<IEnumerable<NoteEntity>, IEnumerable<FullModel>>(storedData);

            var storedData =
                _noteRepository.ExecuteReturedStoredProcedure<NoteEntity>("usp_Notes_GetData", parameters.ToArray());
            var result = _mapper.Map<IEnumerable<NoteEntity>, IEnumerable<FullModel>>(storedData);
            return result;
        }

        public void AddNote(CreateModel note)
        {
            var entity = _mapper.Map<CreateModel, NoteEntity>(note);
            _noteRepository.Add(entity);
            _uow.SaveChanges();
        }

        public EditModel GetNoteForEdit(int id)
        {
            var storedData = _noteRepository.GetSingle(id);
            var result = _mapper.Map<NoteEntity, EditModel>(storedData);
            return result;
        }

        public async Task<EditModel> GetNoteForEditAsync(int id)
        {
            var storedData = await _noteRepository.GetSingleAsync(id);
            var result = _mapper.Map<NoteEntity, EditModel>(storedData);
            return result;
        }

        //public void EditNote(EditModel data)
        //{
        //    var storedData = _noteRepository.GetSingle(data.Id);
        //    storedData.Title = data.Title;
        //    storedData.Description = data.Description;
        //    _noteRepository.Edit(storedData);
        //    _uow.SaveChanges();
        //}

        public bool CheckExisting(int id)
        {
            var result = _noteRepository.CheckExistingById(id);
            return result;
        }

        public void DeleteNote(int id)
        {
            var storedData = _noteRepository.GetSingle(id);
            storedData.Deleted = true;
            _noteRepository.Edit(storedData);
            _uow.SaveChanges();
        }

        public void EditNote(EditModel data)
        {
            throw new NotImplementedException();
        }
    }
}