using System;
using System.Collections.Generic;
using AutoMapper;
using GrooveNoteDAL.Entities;
using GrooveNoteDAL.Models.Note;

namespace GrooveNoteDAL.Mappers
{
    public class NoteAutoMapperProfile : Profile
    {
        public NoteAutoMapperProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<NoteEntity, IndexModel>();
            CreateMap<NoteEntity, EditModel>();
            CreateMap<NoteEntity, FullModel>();

            CreateMap<CreateModel, NoteEntity>();
            CreateMap<EditModel, NoteEntity>();

        }
    }
}
