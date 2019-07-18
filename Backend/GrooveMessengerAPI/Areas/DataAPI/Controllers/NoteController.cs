using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GrooveMessengerDAL.Models.Note;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using System;

namespace GrooveMessengerAPI.Areas.DataAPI.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AllowAnonymous]
    [Route("DataAPI/[controller]")]
    public class NoteController : Controller
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet("")]
        public IEnumerable<FullModel> GetNotes()
        {
            var result = _noteService.GetNoteListFullModel();
            return result;
        }

        [HttpGet("{id}")]
        public async Task<EditModel> GetNote(int id)
        {
            var data = await _noteService.GetNoteForEditAsync(id);
            return data;
        }

        //[HttpPut("{id}")]
        //public EditModel EditNote(Guid id, [FromBody] EditModel note)
        //{
        //    if (id != note.Id)
        //    {
        //        return null;
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        var isExisting = _noteService.CheckExisting(id);
        //        if (!isExisting)
        //        {
        //            return null;
        //        }

        //        _noteService.EditNote(note);
        //        return note;
        //    }

        //    return null;
        //}

        [HttpPost]
        public IActionResult CreateNote([FromBody] CreateModel note)
        {
            if (ModelState.IsValid)
            {
                _noteService.AddNote(note);
            }
            else
            {
                return new BadRequestResult();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNote(int id)
        {
            var isExisting = _noteService.CheckExisting(id);
            if (!isExisting)
            {
                return new NotFoundResult();
            }

            _noteService.DeleteNote(id);
            return Ok();
        }
    }
}