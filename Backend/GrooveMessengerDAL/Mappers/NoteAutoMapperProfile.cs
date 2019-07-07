using System;
using System.Collections.Generic;
using AutoMapper;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.Note;

namespace GrooveMessengerDAL.Mappers
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
